using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BudgetApp.Models;
using BudgetApp.HelperExtensions;
using Microsoft.AspNet.Identity;

namespace BudgetApp.Controllers
{
    [RequireHttps]
    [Authorize]
    [AuthorizeHouseholdRequired]
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var hh = id.GetHousehold();
            var accounts = hh.BankAccounts;
            return View(accounts.OrderByDescending(a=>a.Name).ToList());
        }

        //GET: Transactions Partial
        public PartialViewResult View(int? id)
        {
            BankAccount account = db.BankAccounts.Find(id);
            return PartialView("_Transactions");
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var hh = userId.GetHousehold();

            ViewBag.BankAccountId = new SelectList(hh.BankAccounts, "Id", "Name");
            ViewBag.BudgetItemId = new SelectList(hh.BudgetItems, "Id", "Name");
            ViewBag.CategoryId = new SelectList(hh.Categories, "Id", "Name");

            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BankAccountId,CategoryId,BudgetItemId,UserId,Transacted,Entered,Amount,Description,Type,Reconciled")] Transaction transaction)
        {
            var id = User.Identity.GetUserId();
            var hh = id.GetHousehold();

            if (ModelState.IsValid)
            {
                var account = db.BankAccounts.FirstOrDefault(a => a.Id.Equals(transaction.BankAccountId));
                var budget = db.BudgetItems.FirstOrDefault(b => b.Id.Equals(transaction.BudgetItemId));
                
                //set category
                if (transaction.BudgetItemId!= null)
                {
                    transaction.CategoryId = transaction.BudgetItem.CategoryId;
                }

                //balance calculations
                account.Balance = transaction.GetAccountBalance();
                if (transaction.BudgetItem != null)
                {
                    budget.Balance = transaction.GetBudgetBalance();
                }

                db.SaveChanges();

                //check budget warnings
                //if (budget.AmountLimit - budget.Balance <= )

                //finish up
                transaction.Entered = DateTimeOffset.Now;
                transaction.UserId = id;

                db.Entry(transaction).State = EntityState.Modified;
                db.Transactions.Add(transaction);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.BankAccountId = new SelectList(hh.BankAccounts, "Id", "Name");
            ViewBag.BudgetItemId = new SelectList(hh.BudgetItems, "Id", "Name");
            ViewBag.CategoryId = new SelectList(hh.Categories, "Id", "Name");

            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            var userId = User.Identity.GetUserId();
            var hh = userId.GetHousehold();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }

            //TempData["OriginalAmount"] = transaction.Amount; - use .AsNoTracking() in Post instead

            ViewBag.BankAccountId = new SelectList(hh.BankAccounts, "Id", "Name", transaction.BankAccountId);
            ViewBag.BudgetItemId = new SelectList(hh.BudgetItems, "Id", "Name", transaction.BudgetItemId);
            ViewBag.CategoryId = new SelectList(hh.Categories, "Id", "Name", transaction.CategoryId);

            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BankAccountId,CategoryId,BudgetItemId,UserId,Transacted,Amount,Description,Type,Reconciled")] Transaction transaction)
        {
            var id = User.Identity.GetUserId();
            var hh = id.GetHousehold();

            if (ModelState.IsValid)
            {
                //var original = (decimal)TempData["OriginalAmount"]; - not best practice
                var original = db.Transactions.AsNoTracking().FirstOrDefault(t => t.Id == transaction.Id); 
                var account = db.BankAccounts.FirstOrDefault(a => a.Id.Equals(original.BankAccountId));
                var budget = db.BudgetItems.FirstOrDefault(b => b.Id.Equals(original.BudgetItemId));


                //set category
                if (transaction.BudgetItemId != null)
                {
                    transaction.CategoryId = transaction.BudgetItem.CategoryId;
                }

                //balance calculations
                account.Balance = transaction.RevertAccountBalance(original);
                budget.Balance = transaction.RevertBudgetBalance(original);
                
                account = db.BankAccounts.FirstOrDefault(a=>a.Id.Equals(transaction.BankAccountId));
                budget = db.BudgetItems.FirstOrDefault(b => b.Id.Equals(transaction.BudgetItemId));
                account.Balance = transaction.GetNewAccountBalance();
                budget.Balance = transaction.GetNewBudgetBalance();
                
                //finish up
                transaction.UserId = id;

                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name", transaction.BankAccountId);
            ViewBag.BudgetItemId = new SelectList(db.BudgetItems, "Id", "Name", transaction.BudgetItemId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", transaction.CategoryId);

            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);

            var userId = User.Identity.GetUserId();
            var account = db.BankAccounts.FirstOrDefault(a => a.Id.Equals(transaction.BankAccountId));
            var budget = db.BudgetItems.FirstOrDefault(b => b.Id.Equals(transaction.BudgetItemId));

            //balance calculations
            account.Balance = transaction.GetAccountBalanceOnDelete();
            budget.Balance = transaction.GetBudgetBalanceOnDelete();

            db.Transactions.Remove(transaction);
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
