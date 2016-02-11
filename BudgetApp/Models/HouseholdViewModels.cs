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
        [Required(ErrorMessage ="You must enter a household name.")]
        [StringLength(25, ErrorMessage ="Household names must be between 3 and 25 characters.", MinimumLength =3)]
        public string HouseholdName { get; set; }
        public string AdminUserId { get; set; }
        [Required(ErrorMessage = "You must provide an invite code.")]
        public string InviteCode { get; set; }
        public string InviteEmail { get; set; }
        public ApplicationUser User { get; set; }
    }
}