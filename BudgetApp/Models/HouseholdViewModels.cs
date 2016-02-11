using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetApp.Models
{
    public class CreateHouseholdViewModel
    {
        public int HouseholdId { get; set; }
        [StringLength(25, ErrorMessage ="Household names must be between 3 and 25 characters.", MinimumLength =3)]
        public string HouseholdName { get; set; }
        public string AdminUserId { get; set; }
        public string InviteCode { get; set; }
        public string InviteEmail { get; set; }
        //public ApplicationUser User { get; set; }
    }
}