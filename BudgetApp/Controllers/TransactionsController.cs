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
            var hh = db.Households.Find(Convert.ToInt32(User.Identity.GetHouseholdId()));
            return View(hh.BankAccounts.Where(a=>a.IsSoftDeleted!= true).OrderBy(a=>a.Name).ToList());
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var hh = userId.GetHousehold();

            ViewBag.BAId = new SelectList(hh.BankAccounts.Where(a=>a.IsSoftDeleted!= true), "Id", "Name");
            ViewBag.BIId = new SelectList(hh.BudgetItems.Where(b=>b.IsSoftDeleted!= true), "Id", "Name");
            ViewBag.CId = new SelectList(hh.Categories, "Id", "Name");

            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BankAccountId,CategoryId,BudgetItemId,UserId,Transacted,Entered,Amount,Description,Income,Reconciled,")] Transaction transaction, bool IsIncome, bool IsReconciled)
        {
            var id = User.Identity.GetUserId();
            var hh = id.GetHousehold();

            if (ModelState.IsValid)
            {
                var account = db.BankAccounts.FirstOrDefault(a => a.Id == transaction.BankAccountId);
                var budget = db.BudgetItems.FirstOrDefault(b => b.Id == transaction.BudgetItemId);

                transaction.Reconciled = IsReconciled;
                transaction.Income = IsIncome;

                //set category
                if (transaction.BudgetItemId != null)
                    transaction.CategoryId = budget.CategoryId;
                else if (transaction.BudgetItemId==null&&transaction.CategoryId== null)
                    transaction.CategoryId = hh.Categories.FirstOrDefault(c => c.Name == "Miscellaneous").Id;

                //balance calculations
                account.Balance = transaction.GetAccountBalance();
                if (transaction.BudgetItemId != null) budget.Balance = transaction.GetBudgetBalance();

                db.SaveChanges();

                //check budget warnings
                if (transaction.BudgetItemId != null && (budget.AmountLimit - budget.Balance <= Convert.ToDecimal(budget.Warning.WarningLevel)))
                    { }

                //finish up
                transaction.Entered = DateTimeOffset.Now;
                transaction.UserId = id;

                db.Entry(transaction).State = EntityState.Modified;
                db.Transactions.Add(transaction);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.BAId = new SelectList(hh.BankAccounts.Where(a => a.IsSoftDeleted != true), "Id", "Name");
            ViewBag.BIId = new SelectList(hh.BudgetItems.Where(b => b.IsSoftDeleted != true), "Id", "Name");
            ViewBag.CId = new SelectList(hh.Categories, "Id", "Name");

            return RedirectToAction("Index");
        }

        // GET: Transactions/Edit/5
        public PartialViewResult _Edit(int? id)
        {
            var userId = User.Identity.GetUserId();
            var hh = userId.GetHousehold();
            Transaction transaction = db.Transactions.Find(id);

            //TempData["OriginalAmount"] = transaction.Amount; - use .AsNoTracking() in Post instead

            ViewBag.BAId = new SelectList(hh.BankAccounts.Where(a => a.IsSoftDeleted != true), "Id", "Name");
            ViewBag.BIId = new SelectList(hh.BudgetItems.Where(b => b.IsSoftDeleted != true), "Id", "Name");
            ViewBag.CId = new SelectList(hh.Categories, "Id", "Name", transaction.CategoryId);

            return PartialView(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BankAccountId,CategoryId,BudgetItemId,UserId,Transacted,Amount,Description,Income,Reconciled")] Transaction transaction, bool IsIncome)
        {
            var id = User.Identity.GetUserId();
            var hh = id.GetHousehold();

            if (ModelState.IsValid)
            {
                transaction.Income = IsIncome;
                //var original = (decimal)TempData["OriginalAmount"]; - not best practice
                var original = db.Transactions.AsNoTracking().FirstOrDefault(t => t.Id == transaction.Id); 
                var account = db.BankAccounts.FirstOrDefault(a => a.Id == original.BankAccountId);
                var budget = db.BudgetItems.FirstOrDefault(b => b.Id == original.BudgetItemId);

                //balance calculations
                account.Balance = original.RevertAccountBalance();
                account = db.BankAccounts.FirstOrDefault(a => a.Id == transaction.BankAccountId);
                account.Balance = transaction.GetAccountBalance();

                if (budget != null)
                {
                    budget.Balance = original.RevertBudgetBalance();
                    budget = db.BudgetItems.FirstOrDefault(b => b.Id == transaction.BudgetItemId);
                    budget.Balance = transaction.GetBudgetBalance();
                }

                transaction.UserId = id;
                db.SaveChanges();

                //set category
                transaction.BudgetItem = budget;
                if (transaction.BudgetItemId != null) transaction.CategoryId = transaction.BudgetItem.CategoryId;

                db.SaveChanges();
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BankAccountId = new SelectList(hh.BankAccounts.Where(a => a.IsSoftDeleted != true), "Id", "Name", transaction.BankAccountId);
            ViewBag.BudgetItemId = new SelectList(hh.BudgetItems.Where(b => b.IsSoftDeleted != true), "Id", "Name", transaction.BudgetItemId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", transaction.CategoryId);

            return RedirectToAction("Index");
        }

        // GET: Transactions/Delete/5
        public PartialViewResult _Delete(int? id)
        {
            Transaction transaction = db.Transactions.Find(id);

            return PartialView(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            var userId = User.Identity.GetUserId();
            var account = db.BankAccounts.FirstOrDefault(a => a.Id == transaction.BankAccountId);
            var budget = db.BudgetItems.FirstOrDefault(b => b.Id == transaction.BudgetItemId);

            //balance calculations
            account.Balance = transaction.RevertAccountBalance();
            if (budget != null) budget.Balance = transaction.RevertBudgetBalance();

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
