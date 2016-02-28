using BudgetApp.HelperExtensions;
using BudgetApp.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetApp.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var hh = userId.GetHousehold();

            var accountsList = (from account in db.BankAccounts.Include("Transactions")
                                where account.IsSoftDeleted != true && account.HouseholdId == hh.Id
                             let reconciledI = (from transaction in account.Transactions
                                        where transaction.Reconciled == true &&
                                        transaction.Income == true
                                        select transaction.Amount)
                                        .DefaultIfEmpty().Sum()
                             let reconciledE = (from transaction in account.Transactions
                                                where transaction.Reconciled == true &&
                                                transaction.Income == false
                                                select transaction.Amount)
                                        .DefaultIfEmpty().Sum()
                                select new ReconBankAccount
                             {
                                 Account = account,
                                 ReconciledBalance = reconciledI-reconciledE,                               
                             }).ToList();

            var budgetsList = hh.BudgetItems.Where(b => b.IsSoftDeleted != true);

            var model = new DashboardViewModel
            {
                ReconBankAccounts = accountsList,
                BudgetList = budgetsList
            };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //GET: Home/Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //POST: Home/Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactMessage contact, string returnUrl)
        {
            var es = new EmailService();
            var msg = new IdentityMessage();
            msg.Destination = ConfigurationManager.AppSettings["ContactEmail"];
            msg.Body = "You have been sent a message from " + contact.Name + " (" + contact.Email + ") with the following contents. <br/><br/>\"" + contact.Message + "\"";
            msg.Subject = "Message received through Words from the West";
            es.SendAsync(msg);

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Account", null);
            }
        }

        public ActionResult GetCharts()
        {
            var hh = db.Households.Find(Convert.ToInt32(User.Identity.GetHouseholdId()));

            var accountsOverviewBar = (from account in hh.BankAccounts.Where(a=>a.IsSoftDeleted!=true)
                                      let income = (from transaction in account.Transactions
                                          .Where(t => t.Income == true &&
                                                 t.Transacted.DateTime.Year == DateTime.Now.Year &&
                                                 t.Transacted.DateTime.Month == DateTime.Now.Month)
                                                    select transaction.Amount).DefaultIfEmpty().Sum()
                                      let expense = (from trans in account.Transactions
                                           .Where(t => t.Income == false &&
                                                 t.Transacted.DateTime.Year == DateTime.Now.Year &&
                                                 t.Transacted.DateTime.Month == DateTime.Now.Month)
                                                     select trans.Amount).DefaultIfEmpty().Sum()
                                      select new
                                      {
                                          label = account.Name,
                                          income = income,
                                          expense = expense
                                      }).ToArray();

            var budgetsBar = (from budget in hh.BudgetItems
                              let limit = (budget.AmountLimit)
                              let balance = (from trans in budget.Transactions
                                             .Where(t => t.Transacted.DateTime.Year == DateTime.Now.Year &&
                                                  t.Transacted.DateTime.Month == DateTime.Now.Month)
                                             select trans.Amount).DefaultIfEmpty().Sum()
                              select new
                              {
                                  label = budget.Name,
                                   limit = limit,
                                   balance = balance
                               }).ToArray();


            var expenseDonut = (from category in hh.Categories
                                     let expense = (from transaction in category.Transactions
                                                    where transaction.Income != true &&
                                                    transaction.Transacted.DateTime.Year == DateTime.Now.Year &&
                                                    transaction.Transacted.DateTime.Month == DateTime.Now.Month
                                                    select transaction.Amount).DefaultIfEmpty().Sum()
                                where expense > 0
                                     select new
                                     {
                                         label = category.Name,
                                         value = expense
                                     }).ToArray();

            var incomeDonut = (from category in hh.Categories
                                    let income = (from transaction in category.Transactions
                                                   where transaction.Income == true &&      
                                                   transaction.Transacted.DateTime.Year == DateTime.Now.Year &&
                                                   transaction.Transacted.DateTime.Month == DateTime.Now.Month
                                                  select transaction.Amount).DefaultIfEmpty().Sum()
                               where income > 0
                                    select new
                                    {
                                        label = category.Name,
                                        value = income
                                    }).ToArray();

            //            var accountsHistoryLine = from account in hh.BankAccounts.Where(a=>a.IsSoftDeleted!=true)
            //                                      let income = (from transaction in account.Transactions
            //    .Where(t => t.Income == true &&
            //           t.Transacted.DateTime.Year == DateTime.Now.Year &&
            //           t.Transacted.DateTime.Month == DateTime.Now.Month)
            //                                                    select transaction.Amount).DefaultIfEmpty().Sum()

            //riamang[1:54 PM]
            //var monthsToDate = Enumerable.Range(1, DateTime.Today.Month)
            //                           .Select(m => new DateTime(DateTime.Today.Year, m, 1))
            //                           .ToList();

            var allData = new
            {
                accountsOverviewBar = accountsOverviewBar,
                expenseDonut = expenseDonut,
                incomeDonut = incomeDonut,
                budgetsBar = budgetsBar
            };

            return Content(JsonConvert.SerializeObject(allData), "application/json");
        }



        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}