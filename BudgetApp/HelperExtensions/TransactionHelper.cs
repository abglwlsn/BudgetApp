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

        public static decimal GetAccountBalance(this Transaction transaction, string userId)
        {            
            var user = db.Users.FirstOrDefault(u => u.Id.Equals(userId));
            var account = db.BankAccounts.FirstOrDefault(a => a.Id.Equals(transaction.BankAccountId));

            if (transaction.Type == true)
            {
                account.Balance += transaction.Amount;
            }
            else
            {
                account.Balance -= transaction.Amount;
            }

            return account.Balance;
        }

        public static decimal GetAccountBalance(this Transaction transaction, string userId, decimal original)
        {
            var user = db.Users.FirstOrDefault(u => u.Id.Equals(userId));
            var account = db.BankAccounts.FirstOrDefault(a => a.Id.Equals(transaction.BankAccountId));


            if (transaction.Type == true)
            {
                account.Balance -= original + transaction.Amount;
            }
            else
            {
                account.Balance += original - transaction.Amount;
            }

            return account.Balance;
        }

        public static decimal GetAccountBalanceOnDelete(this Transaction transaction, string userId)
        {
            var user = db.Users.FirstOrDefault(u => u.Id.Equals(userId));
            var account = db.BankAccounts.FirstOrDefault(a => a.Id.Equals(transaction.BankAccountId));

            if (transaction.Type == true)
            {
                account.Balance -= transaction.Amount;
            }
            else
            {
                account.Balance += transaction.Amount;
            }

            return account.Balance;
        }

        public static decimal GetBudgetBalance(this Transaction transaction, string userId)
        {
            var account = db.BankAccounts.FirstOrDefault(a => a.Id.Equals(transaction.BankAccountId));
            var budget = db.BudgetItems.FirstOrDefault(b => b.Id.Equals(transaction.BudgetItemId));

            if (transaction.Type == true)
            {
                if (budget.Type == true)
                {
                    budget.Balance += transaction.Amount;
                }
                else
                {
                    budget.Balance -= transaction.Amount;
                }
            }
            else
            {
                if (budget.Type == true)
                {
                    budget.Balance -= transaction.Amount;
                }
                else
                {
                    budget.Balance += transaction.Amount;
                }
            }
            return budget.Balance;
        }

        public static decimal GetBudgetBalance(this Transaction transaction, string userId, decimal original)
        {
            var account = db.BankAccounts.FirstOrDefault(a => a.Id.Equals(transaction.BankAccountId));
            var budget = db.BudgetItems.FirstOrDefault(b => b.Id.Equals(transaction.BudgetItemId));

            if (transaction.Type == true)
            {
                if (budget.Type == true)
                {
                    budget.Balance -= original + transaction.Amount;
                }
                else
                {
                    budget.Balance += original - transaction.Amount;
                }
            }
            else
            {
                if (budget.Type == true)
                {
                    budget.Balance += original - transaction.Amount;
                }
                else
                {
                    budget.Balance -= original + transaction.Amount;
                }
            }
            return budget.Balance;
        }

        public static decimal GetBudgetBalanceOnDelete(this Transaction transaction, string userId)
        {
            var account = db.BankAccounts.FirstOrDefault(a => a.Id.Equals(transaction.BankAccountId));
            var budget = db.BudgetItems.FirstOrDefault(b => b.Id.Equals(transaction.BudgetItemId));

            if (transaction.Type == true)
            {
                if (budget.Type == true)
                {
                    budget.Balance -= transaction.Amount;
                }
                else
                {
                    budget.Balance += transaction.Amount;
                }
            }
            else
            {
                if (budget.Type == true)
                {
                    budget.Balance += transaction.Amount;
                }
                else
                {
                    budget.Balance -= transaction.Amount;
                }
            }
            return budget.Balance;
        }
    }
}