using System;
using BudgetApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace BudgetApp.HelperExtensions
{
    public static class HouseholdHelpers
    {
        //To use helpers, first instantiate in the desired controller. Then, use helper!
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
    }
}