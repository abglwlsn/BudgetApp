using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BudgetApp.Models;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using System.Configuration;

namespace BudgetApp.Controllers
{
    [RequireHttps]
    [Authorize]
    public class InvitedUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InvitedUsers
        public ActionResult Index()
        {
            var invitedUsers = db.InvitedUsers.Include(i => i.Household);
            return View(invitedUsers.ToList());
        }

        // GET: InvitedUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvitedUser invitedUser = db.InvitedUsers.Find(id);
            if (invitedUser == null)
            {
                return HttpNotFound();
            }
            return View(invitedUser);
        }

        // GET: InvitedUsers/Create
        public ActionResult Create()
        {
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name");
            return View();
        }

        // POST: InvitedUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HouseholdId,Name,Email,AdminRights,InviteCode")] InvitedUser invitedUser)
        {
            if (ModelState.IsValid)
            {
                var id = User.Identity.GetUserId();
                var user = db.Users.FirstOrDefault(u => u.Id.Equals(id));
 
                invitedUser.HouseholdId = user.HouseholdId.GetValueOrDefault();
                invitedUser.InviteCode = Membership.GeneratePassword(10, 4);
                invitedUser.InvitedBy = user.FirstName + " " + user.LastName;

                //if (AdminRights==true)
                //{
                //    give Admin rights
                //}

                //How to force delete of entry after certain time period? Or leave entry but generate new code?
                var es = new EmailService();
                var msg = new IdentityMessage();
                var dt = DateTime.Now.AddDays(7).ToLongDateString();
                msg.Destination = invitedUser.Email; //ConfigurationManager.AppSettings["ContactEmail"];
                msg.Body = invitedUser.InvitedBy + " " + "has invited you to join their household on Cachin' Cash! To access Cachin' Cash's extensive tools for financial management, copy the following Invite Code and then visit the Cachin' Cash website by clicking <a href=\"http://awest-budget.azurewebsites.net\">here</a>. After registering, enter your Invite Code in the indicated text box to join the household. <br/>This code is only active until" + dt + ", after which point you can request a new code from " + invitedUser.InvitedBy + ".<br/><br/>Invite Code:" + invitedUser.InviteCode;
                msg.Subject = "Invitation to join Cachin' Cash";
                es.SendAsync(msg);

                db.InvitedUsers.Add(invitedUser);
                db.SaveChanges();
                return RedirectToAction("Details", "Households", (new { id = user.HouseholdId }));
            }

            //ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invitedUser.HouseholdId);
            return View(invitedUser);
        }
        

        // GET: InvitedUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvitedUser invitedUser = db.InvitedUsers.Find(id);
            if (invitedUser == null)
            {
                return HttpNotFound();
            }
            return View(invitedUser);
        }

        // POST: InvitedUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvitedUser invitedUser = db.InvitedUsers.Find(id);
            db.InvitedUsers.Remove(invitedUser);
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
