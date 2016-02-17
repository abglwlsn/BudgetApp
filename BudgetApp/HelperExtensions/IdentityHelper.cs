using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace BudgetApp.HelperExtensions
{
    public static class IdentityHelper
    {
        public static string GetHouseholdId(this IIdentity user)
        {
            var ClaimUser = (ClaimsIdentity)user;
            var Claim = ClaimUser.Claims.FirstOrDefault(c => c.Type == "HouseholdId"); //go through all claims and take first to match the name we gave the claim
            if (Claim != null)
                return Claim.Value;
            else
                return null;
        }

        public static bool IsUserInHousehold(this IIdentity user)
        {
            var ClaimUser = (ClaimsIdentity)user;
            var Claim = ClaimUser.Claims.FirstOrDefault(c => c.Type == "HouseholdId");
            if (Claim.Value != null)
                return true;
            else
                return false;
        }
    }
}