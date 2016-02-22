using BudgetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetApp.HelperExtensions
{
    public static class TransactionHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        //Account Balances
        public static decimal GetAccountBalance(this Transaction transaction)
        {
            return ModifyAccountBalance(transaction, false);
        }

        public static decimal RevertAccountBalance(this Transaction transaction)
        {
            return ModifyAccountBalance(transaction, true);
        }

        private static decimal ModifyAccountBalance(this Transaction transaction, bool Delete)
        {
            var account = db.BankAccounts.FirstOrDefault(a => a.Id == transaction.BankAccountId);
            bool AddMoney;

            if (Delete) AddMoney = !transaction.Income; 
            else AddMoney = transaction.Income;

            if (AddMoney == true)
                account.Balance += transaction.Amount;
            else
                account.Balance -= transaction.Amount;

            return account.Balance;
        }

        //Budget Balances
        public static decimal GetBudgetBalance(this Transaction transaction)
        {
            return ModifyBudgetBalance(transaction, false);
        }

        public static decimal RevertBudgetBalance(this Transaction transaction)
        {
            return ModifyBudgetBalance(transaction, true);
        }

        private static decimal ModifyBudgetBalance(this Transaction transaction, bool Delete)
        {
            var budget = db.BudgetItems.FirstOrDefault(b => b.Id == transaction.BudgetItemId);
            bool AddMoney;

            if (Delete) AddMoney = !transaction.Income;
            else AddMoney = transaction.Income;

            if (AddMoney == true)
            {
                if (budget.Income == true)
                    budget.Balance += transaction.Amount;
                else budget.Balance -= transaction.Amount;
            }
            else
            {
                if (budget.Income == true)
                    budget.Balance -= transaction.Amount;
                else budget.Balance += transaction.Amount;
            }            
            return budget.Balance;            
        }
    }
}