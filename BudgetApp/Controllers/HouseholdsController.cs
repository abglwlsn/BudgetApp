using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BudgetApp.Models;

namespace BudgetApp.Controllers
{
    [RequireHttps]
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Households
        public ActionResult Index()
        {
            return View(db.Households.ToList());
        }

        // GET: Households/Details/5
        public ActionResult Details(int? id)
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

        // GET: Households/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateHouseholdViewModel model)
        {
            Household household = new Household();

            model.HouseholdName = household.Name;
            model.AdminUserId = household.AdminUserId;

            if (ModelState.IsValid)
            {
                if (model.HouseholdName != null)
                {
                    household.AdminUserId = User.Identity.GetUserId();
                    db.Households.Add(household);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Households");
                }
            }
            return View(household);
        }

        //POST: Households/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Join(CreateHouseholdViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.InviteCode != null)
                {
                    var Iuser = db.InvitedUsers.Where(u => u.InviteCode.Equals(model.InviteCode) && u.Email.Equals(model.InviteEmail));

                    if (Iuser != null)
                    {
                        //Household household = new Household;
                        var user = household.Users.FirstOrDefault(u => u.Email.Equals(model.InviteEmail));
                        model.User.HouseholdId = user.HouseholdId;
                        household.Users.Add(user);

                        db.SaveChanges();
                        return RedirectToAction("Details", "Households");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Sorry, the invite code and email do not match.";
                        return View(model);
                    }
                }
            }

            return View(model);
        }





        // GET: Households/Edit/5
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
        public ActionResult Edit([Bind(Include = "Id,Name,Published,AdminUserId")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(household);
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Household household = db.Households.Find(id);
            db.Households.Remove(household);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
