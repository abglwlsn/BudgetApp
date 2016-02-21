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

        private static decimal ModifyAccountBalance(this Transaction transaction, bool Delete)
        {
            var account = db.BankAccounts.FirstOrDefault(a => a.Id == transaction.BankAccountId);

            //bool AddMoney = (transaction.Income != Delete);
            //if Income is true and it's not a delete, AddMoney true
            //If Income is false and it's not a delete, AddMoney false
            //If Income is true and it is a delete , AddMoney false
            //If Income is false and it is a delete, AddMonet is True

            bool AddMoney;
            if (Delete) AddMoney = !transaction.Income; 
            else AddMoney = transaction.Income;


            if (AddMoney == true)
                account.Balance += transaction.Amount;
            else
                account.Balance -= transaction.Amount;

            return account.Balance;
        }

        public static decimal GetAccountBalance(this Transaction transaction)
        {
            return ModifyAccountBalance(transaction, false);
        }
        public static decimal RevertAccountBalance(this Transaction transaction)
        {
            return ModifyAccountBalance(transaction, true);
        }


        //

        public static decimal GetBudgetBalance(this Transaction transaction)
        {
            var budget = db.BudgetItems.FirstOrDefault(b => b.Id == transaction.BudgetItemId);

            if (transaction.Income == true)
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

            if (transaction.Income == true )
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

            if (transaction.Income == true)
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

            if (transaction.Income == true)
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