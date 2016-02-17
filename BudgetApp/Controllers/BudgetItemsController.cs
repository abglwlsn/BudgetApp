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
    public class BudgetItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BudgetItems
        public ActionResult Index()
        {
            var hId = User.Identity.GetHouseholdId();
            var budgetItems = db.BudgetItems.Where(h => h.HouseholdId.Equals(hId));
            return View(budgetItems.ToList());
        }

        // GET: BudgetItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetItem budgetItem = db.BudgetItems.Include("Transactions").FirstOrDefault(b=>b.Id == id);
            if (budgetItem == null)
            {
                return HttpNotFound();
            }
            return View(budgetItem);
        }

        // GET: BudgetItems/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.DurationId = new SelectList(db.Durations, "Id", "Length");

            return View();
        }

        // POST: BudgetItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,HouseholdId,Name,AmountLimit,Balance,Type,WarnAtId,CreatorId,DurationId,AllowEdits")] BudgetItem budgetItem)
        {
            if (ModelState.IsValid)
            {
                budgetItem.HouseholdId = Convert.ToInt32(User.Identity.GetHouseholdId());
                budgetItem.Balance = 0;
                budgetItem.CreatorId = User.Identity.GetUserId();

                db.BudgetItems.Add(budgetItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", budgetItem.CategoryId);
            ViewBag.DurationId = new SelectList(db.Durations, "Id", "Length", budgetItem.DurationId);

            return View(budgetItem);
        }

        // GET: BudgetItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BudgetItem budgetItem = db.BudgetItems.Find(id);

            if (budgetItem == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", budgetItem.CategoryId);
            ViewBag.DurationId = new SelectList(db.Durations, "Id", "Length", budgetItem.DurationId);
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", budgetItem.HouseholdId);

            return View(budgetItem);
        }

        // POST: BudgetItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,Name,AmountLimit,DurationId,Type,WarnAtId,AllowEdits")] BudgetItem budgetItem)
        { 
                if (ModelState.IsValid)
                {
                    db.Entry(budgetItem).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", budgetItem.CategoryId);
            ViewBag.DurationId = new SelectList(db.Durations, "Id", "Length", budgetItem.DurationId);
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", budgetItem.HouseholdId);

            return View(budgetItem);
        }

        // GET: BudgetItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetItem budgetItem = db.BudgetItems.Find(id);
            if (budgetItem == null)
            {
                return HttpNotFound();
            }
            return View(budgetItem);
        }

        // POST: BudgetItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BudgetItem budgetItem = db.BudgetItems.Find(id);
            db.BudgetItems.Remove(budgetItem);
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
