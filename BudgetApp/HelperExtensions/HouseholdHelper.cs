using System;
using BudgetApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Microsoft.AspNet.Identity;

namespace BudgetApp.HelperExtensions
{
    public static class HouseholdHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static Household GetHousehold(this string userId)
        {
            var user = db.Users.Find(userId);
            if(user== null || user.HouseholdId == null)
            {
                return null;
            }

            var hh = db.Households.Find(user.HouseholdId);

            return hh; //returns entire household
        }

        public static IdentityMessage CreateJoinMessage(this InvitedUser user )
        {
            var invitedUser = db.InvitedUsers.FirstOrDefault(u => u.Id == user.Id);
            var msg = new IdentityMessage();
            var dt = DateTime.Now.AddDays(7).ToLongDateString();
            msg.Destination = invitedUser.Email; //ConfigurationManager.AppSettings["ContactEmail"];
            msg.Body = invitedUser.InvitedBy + " " + "has invited you to join their household on Cachin' Cash! To access Cachin' Cash's extensive tools for financial management, copy the following Invite Code and then visit the Cachin' Cash website by clicking <a href=\"http://awest-budget.azurewebsites.net\">here</a>. After registering, enter your Invite Code in the indicated text box to join the household. <br/>This code is only active until" + dt + ", after which point you can request a new code from " + invitedUser.InvitedBy + ".<br/><br/>Invite Code:" + invitedUser.InviteCode;
            msg.Subject = "Invitation to join Cachin' Cash";

            return msg;
        }

        public static List<Category> AddStandardCategories(this List<CategoryStandard> categories, Household household)
        {
            var CatList = new List<Category>();
            foreach (var category in categories)
            {
                var newCategory = new Category()
                {
                    Name = category.Name,
                    HouseholdId = household.Id
                };
                CatList.Add(newCategory);
            }
            return CatList;
        }
    }
}