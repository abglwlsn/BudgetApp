using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetApp.Models
{
    public class BudgetItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide a category for reporting purposes.")]
        public int CategoryId { get; set; }
        public int HouseholdId { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Budget item names must not exceed 25 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "A budgeted amount is required for each budget item.")]
        public decimal Amount { get; set; }
        [Required]
        [Display(Name="Budget Resets After: ")]
        public int DurationId { get; set; }
        public int CreatorId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Household Household { get; set; }
        public virtual Duration Duration { get; set; }
        public virtual ApplicationUser Creator { get; set; }
    }
}