using BudgApp.Data;
using BudgApp.Data.Models;
using BudgApp.Models.Income;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgApp.Service
{
    public class IncomeService
    {
        private readonly Guid _userID;

        public IncomeService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateIncome(IncomeCreate model) //creates income
        {
            var entity = new Income()
            {
                AccountID = _userID,
                IncomeAmount = model.IncomeAmount,
                IncomeDate = model.IncomeDate
            };

            using (var ctx = new ApplicationDbContext()) 
            {
                ctx.Incomes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<IncomeListItem> GetIncome() // gets all incomes
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Incomes
                  .Where(e => e.AccountID == _userID)
                 .Select(e => new IncomeListItem
                 {
                     IncomeID = e.IncomeID,
                     IncomeAmount = e.IncomeAmount,
                     IncomeDate = e.IncomeDate

                 });
                return query.ToArray();
            }
        }

        public IEnumerable<IncomeListItem> GetIncomeByMonth(DateTime monthKey) // Gets the incomes of a specified month
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Incomes
                    .Where(e => e.IncomeDate.Month == monthKey.Month && e.AccountID == _userID && e.IncomeDate.Year == monthKey.Year).Select(e => new IncomeListItem
                    {
                        IncomeID = e.IncomeID,
                        IncomeAmount = e.IncomeAmount,
                        IncomeDate = e.IncomeDate,
                    });
                return entity.ToArray();
            }

        }
        public IncomeDetails GetIncomeByID(int id) // gets income based on ID
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Incomes
                    .Single(e => e.IncomeID == id && e.AccountID == _userID);
                return new IncomeDetails
                {
                    IncomeID = entity.IncomeID,
                    IncomeAmount = entity.IncomeAmount,
                    IncomeDate = entity.IncomeDate
                };
            }
        }

        public bool UpdateIncome(IncomeEdit model) // updates existing income
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Incomes
                    .Single(e => e.IncomeID == model.IncomeID && e.AccountID == _userID);

                entity.IncomeID = model.IncomeID;
                entity.IncomeAmount = model.IncomeAmount;
                entity.IncomeDate = model.IncomeDate;

                return ctx.SaveChanges() == 1;
            }

        }

        public bool DeleteIncome(int incomeID) // deletes selected income
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Incomes
               .Single(e => e.IncomeID == incomeID && e.AccountID == _userID);

                ctx.Incomes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
