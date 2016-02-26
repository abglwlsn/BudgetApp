using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetApp.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<ReconBankAccount> ReconBankAccounts { get; set; }
        public IEnumerable<BudgetItem> BudgetList { get; set; }
    }

    public class ManageAccountsViewModel
    {
        public IEnumerable<ReconBankAccount> ReconBankAccounts { get; set; }
    }
}