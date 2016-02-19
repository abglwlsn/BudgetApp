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

        public static decimal GetAccountBalance(this Transaction transaction)
        {            
            var account = db.BankAccounts.FirstOrDefault(a => a.Id == transaction.BankAccountId);

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

        public static decimal RevertAccountBalance(this Transaction transaction, Transaction original)
        {
            var account = db.BankAccounts.FirstOrDefault(a => a.Id == original.BankAccountId);

            if (transaction.Type == true)
            {
                account.Balance -= original.Amount;
            }
            else
            {
                account.Balance += original.Amount;
            }
            return account.Balance;
        }
        public static decimal GetNewAccountBalance(this Transaction transaction)
        {
            var account = db.BankAccounts.FirstOrDefault(a => a.Id == transaction.BankAccountId);

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

        public static decimal GetAccountBalanceOnDelete(this Transaction transaction)
        {
            var account = db.BankAccounts.FirstOrDefault(a => a.Id == transaction.BankAccountId);

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

        public static decimal GetBudgetBalance(this Transaction transaction)
        {
            var budget = db.BudgetItems.FirstOrDefault(b => b.Id == transaction.BudgetItemId);

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

        public static decimal GetNewBudgetBalance(this Transaction transaction)
        {
            var budget = db.BudgetItems.FirstOrDefault(b => b.Id == transaction.BudgetItemId);

            if (transaction.Type == true )
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

        public static decimal RevertBudgetBalance(this Transaction transaction, Transaction original)
        {
            var budget = db.BudgetItems.FirstOrDefault(b => b.Id == original.BudgetItemId);

            if (transaction.Type == true)
            {
                if (budget.Type == true)
                {
                    budget.Balance -= original.Amount;
                }
                else
                {
                    budget.Balance += original.Amount;
                }
            }
            else
            {
                if (budget.Type == true)
                {
                    budget.Balance += original.Amount;
                }
                else
                {
                    budget.Balance -= original.Amount;
                }
            }
            return budget.Balance;
        }

        public static decimal GetBudgetBalanceOnDelete(this Transaction transaction)
        {
            var account = db.BankAccounts.FirstOrDefault(a => a.Id == transaction.BankAccountId);
            var budget = db.BudgetItems.FirstOrDefault(b => b.Id == transaction.BudgetItemId);

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