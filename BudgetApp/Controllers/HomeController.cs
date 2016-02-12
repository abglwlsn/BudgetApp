using BudgetApp.Models;
using Microsoft.AspNet.Identity;
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
        public ActionResult Index()
        {
            return View();
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