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
using BudgetApp.HelperExtensions;

namespace BudgetApp.Controllers
{
    [RequireHttps]
    [Authorize]
    [AuthorizeHouseholdRequired]
    public class BankAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BankAccounts
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            Household hh = id.GetHousehold();

            IEnumerable<BankAccount> bankAccounts = db.BankAccounts.Where(b=>b.HouseholdId == hh.Id);
            return View(bankAccounts.ToList());
        }

        // GET: BankAccounts/Details/5
        public ActionResult Details(int? id)
        {
            BankAccount bankAccount = db.BankAccounts.Find(id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        public PartialViewResult _Create()
        {
            var input = TempData["formInput"];

            return PartialView(input);
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Balance")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                bankAccount.HouseholdId = Convert.ToInt32(User.Identity.GetHouseholdId());
                db.BankAccounts.Add(bankAccount);
                db.SaveChanges();

                Transaction originalTransaction = new Transaction()
                {
                    BankAccountId = bankAccount.Id,
                    UserId = User.Identity.GetUserId(),
                    Category = db.Categories.FirstOrDefault((m=>m.Name == "Miscellaneous")),
                    Transacted = DateTimeOffset.Now,
                    Entered = DateTimeOffset.Now,
                    Amount = bankAccount.Balance,
                    Description = "starting balance",
                    Income = true,
                    Reconciled = true
                };
                bankAccount.Transactions.Add(originalTransaction);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            TempData["formInput"] = bankAccount;

            return RedirectToAction("Index");
        }

        // GET: BankAccounts/_Edit/5
        public PartialViewResult _Edit(int? id)
        {
            BankAccount bankAccount = db.BankAccounts.Find(id);
            return PartialView(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bankAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bankAccount);
        }

        // GET: BankAccounts/_Delete/5
        public PartialViewResult _Delete(int? id)
        {
            BankAccount bankAccount = db.BankAccounts.Find(id);
            return PartialView(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankAccount bankAccount = db.BankAccounts.Find(id);
            db.BankAccounts.Remove(bankAccount);
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
