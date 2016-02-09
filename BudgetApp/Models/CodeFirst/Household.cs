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
        public bool Published { get; set; }
        public int AdminUserId { get; set; }
    }
}