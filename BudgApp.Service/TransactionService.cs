using BudgApp.Data;
using BudgApp.Data.Models;
using BudgApp.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using BudgApp.Models.Income;

namespace BudgApp.Service
{
    public class TransactionService
    {
        private readonly Guid _userID;
        public TransactionService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateTransaction(TransactionCreate model) // Creates a transaction. Nice!
        {
            var entity =
                new Transaction()
                {
                    AccountID = _userID,
                    CategoryID = model.CategoryID,
                    Category = model.Category,
                    TransactionAmount = model.TransactionAmount,
                    TransactionDate = model.TransactionDate
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Transactions.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TransactionListItem> GetTransactions() // Gets all of the transactions
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Transactions
                    .Where(e => e.AccountID == _userID)
                    .Select(
                        e =>
                            new TransactionListItem
                            {
                                TransactionID = e.TransactionID,
                                TransactionAmount = e.TransactionAmount,
                                TransactionDate = e.TransactionDate,
                                CategoryID = e.CategoryID,
                                Category = e.Category
                            }
                        );
                return query.ToArray();
            }
        }

        public TransactionDetail GetTransactionByID(int id) // Gets a transaction based on a given ID
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Transactions
                    .Single(e => e.TransactionID == id && e.AccountID == _userID);
                return
                   new TransactionDetail
                   {
                       TransactionID = entity.TransactionID,
                       TransactionAmount = entity.TransactionAmount,
                       TransactionDate = entity.TransactionDate,
                       CategoryID = entity.CategoryID,
                       Category = entity.Category
                   };
            }
        }

        public IEnumerable<TransactionListItem> GetTransactionsByMonth(DateTime monthKey) // Gets the transactions of a specified month
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Transactions
                    .Where(e => e.TransactionDate.Month == monthKey.Month && e.AccountID == _userID && e.TransactionDate.Year == monthKey.Year).Select(e => new TransactionListItem
                    {
                        TransactionID = e.TransactionID,
                        TransactionAmount = e.TransactionAmount,
                        TransactionDate = e.TransactionDate,
                        CategoryID = e.CategoryID,
                        Category = e.Category
                    });
                return entity.ToArray();
            }
            
        }

        public bool UpdateTransaction(TransactionEdit model) // Updates the selected transaction
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionID == model.TransactionID && e.AccountID == _userID);
                entity.TransactionAmount = model.TransactionAmount;
                entity.TransactionDate = model.TransactionDate;
                entity.CategoryID = model.CategoryID;
                entity.Category = model.Category;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTransaction(int TransactionID)// Deletes a selected transaction
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Transactions.Single(e => e.TransactionID == TransactionID && e.AccountID == _userID);
                ctx.Transactions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }


        public float GetTransactionSum (IEnumerable<TransactionListItem> transactionList)
        {

            var listOfSums = new List<float>();
            foreach (TransactionListItem transaction in transactionList)
            {
                var sum = transaction.TransactionAmount;
                listOfSums.Add(sum);
            }

            return listOfSums.Sum();
        }

        public float GetIncomeSum(IEnumerable<IncomeListItem> incomeList)
        {
            var listOfSums = new List<float>();
            foreach (IncomeListItem income in incomeList)
            {
                var sum = income.IncomeAmount;
                listOfSums.Add(sum);
            }

            return listOfSums.Sum();
        }


        public float GetBalance(DateTime monthKey)
        {
            var incomeService = new IncomeService(_userID);
            var incomeArray = incomeService.GetIncomeByMonth(monthKey);
            var transactionArray = GetTransactionsByMonth(monthKey);
            var incomeSum = GetIncomeSum(incomeArray);
            var transactionSum = GetTransactionSum(transactionArray);

            return incomeSum - transactionSum;


        }
    }
}
