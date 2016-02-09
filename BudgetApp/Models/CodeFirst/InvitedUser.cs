using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetApp.Models
{
    public class InvitedUser
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        [Required(ErrorMessage = "Please provide an email address for the invited party.")]
        [EmailAddress]
        public string Email { get; set; }
        public int InviteCode { get; set; }

        public virtual Household Household { get; set; }
    }
}