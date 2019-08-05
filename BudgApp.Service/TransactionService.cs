using BudgApp.Data;
using BudgApp.Data.Models;
using BudgApp.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgApp.Service
{
    public class TransactionService
    {
        private readonly Guid _userID;
        public TransactionService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateTransaction(TransactionCreate model) //Creates a transaction. Nice!
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

        public IEnumerable<TransactionListItem> GetTransactions() // This thing gets the transactions. All of them.
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

        public TransactionDetail GetTransactionByID(int id) //This one gets a transaction based on a given ID. aww ye. 
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

        public IEnumerable<TransactionListItem> GetTransactionsByMonth(DateTime monthKey) // this one gets all the transaction in a certain month. This one took some thought, but turned out ok.
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Transactions
                    .Where(e => e.TransactionDate.Month == monthKey.Month && e.AccountID == _userID).Select(e => new TransactionListItem
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

        public bool UpdateTransaction(TransactionEdit model) //this one updates the transaction. Its pretty lit.
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

        public bool DeleteTransaction(int TransactionID)// if you want to give a transaction the boot, this method is the one. it does its best.
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Transactions.Single(e => e.TransactionID == TransactionID && e.AccountID == _userID);
                ctx.Transactions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
