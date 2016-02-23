using BudgetApp.HelperExtensions;
using BudgetApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetApp.Controllers
{
    [RequireHttps]
    [Authorize]
    [AuthorizeHouseholdRequired]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories/Delete/5
        public PartialViewResult _CreateCat()
        {
            var userId = User.Identity.GetUserId();
            var hh = userId.GetHousehold();
            return PartialView(hh.Categories);
        }

        //POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,HouseholdId")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.HouseholdId = Convert.ToInt32(User.Identity.GetHouseholdId());

                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Details", "Households");
            }
            return RedirectToAction("Details", "Households");
        }

        //GET: Categories/_Edit/5
        public PartialViewResult _Edit(int? id)
        {
            Category category = db.Categories.Find(id);

            return PartialView(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HouseholdId,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                var original = db.Categories.AsNoTracking().FirstOrDefault(c => c.Id == category.Id);

                if (category.Name != original.Name)
                {
                    var transactions = db.Transactions.Where(t => t.Category.Name == original.Name);
                    foreach (var trans in transactions)
                    { trans.Category.Name = category.Name; }
                }              

                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Households");
            }

            return View("Details", "Households");
        }

        // GET: Categories/Delete/5
        public PartialViewResult _Delete(int? id)
        {
            Category category = db.Categories.Find(id);

            return PartialView(category);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            var transactions = db.Transactions.Where(t => t.CategoryId == id);
            var misc = db.Categories.FirstOrDefault(c => c.Name == "Miscellaneous").Id;

            foreach (var transaction in transactions) transaction.CategoryId = misc;

            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Details", "Households");
        }


    }
}