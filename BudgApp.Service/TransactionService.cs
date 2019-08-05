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

        public bool CreateTransaction(TransactionCreate model)
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

        public IEnumerable<TransactionListItem> GetTransactions()
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

        public TransactionDetail GetTransactionByID(int id)
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
    }
}
