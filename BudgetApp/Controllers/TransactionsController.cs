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
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.BankAccount).Include(t => t.BudgetItem).Include(t => t.Category).Include("Universals");
            return View(transactions.OrderByDescending(d=>d.Transacted).ToList());
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
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name");
            ViewBag.BudgetItemId = new SelectList(db.BudgetItems, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories.Include("Universals"), "Id", "Name");

            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BankAccountId,CategoryId,BudgetItemId,UserId,Transacted,Entered,Amount,Description,Type,Reconciled")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var id = User.Identity.GetUserId();
                var user = db.Users.FirstOrDefault(u=>u.Id.Equals(id));
                var account = db.BankAccounts.FirstOrDefault(a => a.Id.Equals(transaction.BankAccountId));
                var budget = db.BudgetItems.FirstOrDefault(b => b.Id.Equals(transaction.BudgetItemId));
                //var warning = db.Durations. FirstOrDefault(w => w.WarnAtId == transaction.BudgetItem.WarnAtId);
                
                //set category
                if (transaction.BudgetItemId!= null)
                {
                    transaction.CategoryId = transaction.BudgetItem.CategoryId;
                }

                //balance calculations
                if (transaction.Type== true)
                {
                    account.Balance = account.Balance + transaction.Amount;

                    if (budget.Type == true)
                    {
                        budget.Balance = budget.Balance + transaction.Amount;
                    }
                    else
                    {
                        budget.Balance = budget.Balance - transaction.Amount;
                    }
                    db.SaveChanges();
                }
                else
                {
                    account.Balance = account.Balance - transaction.Amount;

                    if (budget.Type == true)
                    {
                        budget.Balance = budget.Balance - transaction.Amount;
                    }
                    else
                    {
                        budget.Balance = budget.Balance + transaction.Amount;
                    }
                    db.SaveChanges();
                }

                //check budget warnings
                //if (budget.AmountLimit - budget.Balance <= )

                //finish up
                transaction.Entered = DateTimeOffset.Now;
                transaction.UserId = user.Id;

                db.Entry(transaction).State = EntityState.Modified;
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
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

            TempData["OriginalAmount"] = transaction.Amount;

            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name", transaction.BankAccountId);
            ViewBag.BudgetItemId = new SelectList(db.BudgetItems, "Id", "Name", transaction.BudgetItemId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", transaction.CategoryId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BankAccountId,CategoryId,BudgetItemId,UserId,Transacted,Amount,Description,Type,Reconciled")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var original = (decimal)TempData["OriginalAmount"];
                var id = User.Identity.GetUserId();
                var user = db.Users.FirstOrDefault(u => u.Id.Equals(id));
                var account = db.BankAccounts.FirstOrDefault(a => a.Id.Equals(transaction.BankAccountId));
                var budget = db.BudgetItems.FirstOrDefault(b => b.Id.Equals(transaction.BudgetItemId));

                //set category
                if (transaction.BudgetItemId != null)
                {
                    transaction.CategoryId = transaction.BudgetItem.CategoryId;
                }

                //balance calculations
                if (transaction.Type == true)
                {
                    account.Balance = account.Balance - original + transaction.Amount;

                    if (budget.Type == true)
                    {
                        budget.Balance = budget.Balance - original + transaction.Amount;
                    }
                    else
                    {
                        budget.Balance = budget.Balance + original - transaction.Amount;
                    }
                    db.SaveChanges();
                }
                else
                {
                    account.Balance = account.Balance + original - transaction.Amount;

                    if (budget.Type == true)
                    {
                        budget.Balance = budget.Balance + original - transaction.Amount;
                    }
                    else
                    {
                        budget.Balance = budget.Balance - original + transaction.Amount;
                    }
                    db.SaveChanges();
                }

                //finish up
                transaction.UserId = user.Id;

                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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

            var user = db.Users.FirstOrDefault(u => u.Id.Equals(userId));
            var account = db.BankAccounts.FirstOrDefault(a => a.Id.Equals(transaction.BankAccountId));
            var budget = db.BudgetItems.FirstOrDefault(b => b.Id.Equals(transaction.BudgetItemId));

            //balance calculations
            if (transaction.Type == true)
            {
                account.Balance = account.Balance + transaction.Amount;

                if (budget.Type == true)
                {
                    budget.Balance = budget.Balance  - transaction.Amount;
                }
                else
                {
                    budget.Balance = budget.Balance + transaction.Amount;
                }
                db.SaveChanges();
            }
            else
            {
                account.Balance = account.Balance +  transaction.Amount;

                if (budget.Type == true)
                {
                    budget.Balance = budget.Balance + transaction.Amount;
                }
                else
                {
                    budget.Balance = budget.Balance - transaction.Amount;
                }
                db.SaveChanges();
            }

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
