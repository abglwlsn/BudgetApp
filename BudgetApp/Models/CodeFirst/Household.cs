using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetApp.Models
{
    public class Household
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AdminUserId { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}