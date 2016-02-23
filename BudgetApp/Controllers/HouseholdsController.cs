using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BudgetApp.HelperExtensions;
using BudgetApp.Models;
using System.Threading.Tasks;
using System.Web.Security;

namespace BudgetApp.Controllers
{
    [RequireHttps]
    [Authorize]
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Households
        public ActionResult Index()
        {
            var hId = User.Identity.GetHouseholdId();
            return View(hId);
            //return View(db.Households.ToList());
        }

        // GET: Households/Details/5
        public ActionResult Details()
        {
            var hId = Convert.ToInt32(User.Identity.GetHouseholdId());
            var hh = db.Households.Find(hId);

            ViewBag.UserId = new SelectList(hh.Users, "Id", "Name");

            if (hh == null)
            {
                return RedirectToAction("Create");
            }
            return View(hh);
        }

        // GET: Households/Create
        public ActionResult Create()
        {
            ViewBag.JoinErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id, Name")]Household household)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(u=>u.Id == userId);
            
            if (ModelState.IsValid)
            {
                if (user.HouseholdId != null)
                {
                    ViewBag.ErrorMessage = "You can only belong to one household at a time. If you would  like to create a new household, please leave your current household.";
                return View();
                }

                userId.AddSuperUser();
                user.IsSuperUser = true;
                userId.AddUserToAdmin();
                user.HasAdminRights = true;
                db.Households.Add(household);
                db.SaveChanges();

                //add standard categories to category table
                var hh = userId.GetHousehold();
                var categories = db.CategoryStandards.ToList();
                categories.AddStandardCategories(hh);

                user.HouseholdId = household.Id;
                db.SaveChanges();

                await ControllerContext.HttpContext.RefreshAuthentication(user);

                return RedirectToAction("Details", "Households", new { id = household.Id });
            }
            return View();
        }

        //POST: Households/Join
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Join(string InviteCode)
        {
            if (ModelState.IsValid)
            {
                if (InviteCode != null)
                {
                    var email = User.Identity.GetUserName();
                    var Iuser = db.InvitedUsers.FirstOrDefault(u => u.InviteCode == InviteCode && u.Email == email);

                    if (Iuser != null)
                    {
                        var user = db.Users.FirstOrDefault(u => u.Email == Iuser.Email);

                        user.HouseholdId = Iuser.HouseholdId;
                        user.HasAdminRights = Iuser.HasAdminRights;
                        user.IsSuperUser = false;

                        db.SaveChanges();

                        if (user.HasAdminRights)
                            user.Id.AddUserToAdmin(); 

                        TempData["ErrorMessage"] = "";
                        db.InvitedUsers.Remove(Iuser);
                        db.SaveChanges();

                        await ControllerContext.HttpContext.RefreshAuthentication(user);

                        return RedirectToAction("Details", "Households", new { id = user.HouseholdId });
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Sorry, the invite code and email do not match.";
                        return RedirectToAction("Create");
                    }
                }
            }

            return RedirectToAction("Create");
        }

        //POST: Households/LeaveHousehold
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeHouseholdRequired]
        public async Task<ActionResult> LeaveHousehold(string id)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
                ApplicationUser selectedUser = db.Users.Find(id);
                var hh = currentUser.Household;

                selectedUser.HouseholdId = null;
                db.SaveChanges();

                if (currentUser.Id==selectedUser.Id)
                {
                    //delete entire household if Superuser
                    if(User.IsInRole("SuperUser"))
                    {
                        foreach (var user in hh.Users)
                            user.HouseholdId = null;
                    }

                    await ControllerContext.HttpContext.RefreshAuthentication(currentUser);
                    return RedirectToAction("Create", "Households");
                }
                else
                {
                    return RedirectToAction("Details", "Households", new { id = currentUser.HouseholdId });
                }
            }
            return View();
        }

        // GET: Households/Edit/5
        [AuthorizeHouseholdRequired]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeHouseholdRequired]
        public ActionResult Edit([Bind(Include = "Id,Name")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(household);
        }

        //POST: Households/ChangeAdmin
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeHouseholdRequired]
        public ActionResult ChangeAdmin(string id)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = db.Users.Find(id);
                ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());

                if (user != currentUser)
                {
                    if (user.HasAdminRights == true)
                    {
                        user.HasAdminRights = false;
                        id.RemoveUserFromAdmin();
                    }
                    else
                    {
                        user.HasAdminRights = true;
                        id.AddUserToAdmin();
                    }

                    db.SaveChanges();
                    return RedirectToAction("Details", "Households", new { id = user.HouseholdId });
                }
            }
            return RedirectToAction("Details", "Households");
        }

        //POST: Households/ChangeSuperUser
        [HttpPost]
        [ValidateAntiForgeryToken]

        //// GET: Households/Delete/5
        //[AuthorizeHouseholdRequired]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Household household = db.Households.Find(id);
        //    if (household == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(household);
        //}

        //// POST: Households/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //[AuthorizeHouseholdRequired]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Household household = db.Households.Find(id);
        //    db.Households.Remove(household);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
