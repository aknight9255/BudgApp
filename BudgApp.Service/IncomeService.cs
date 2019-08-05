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

        public bool CreateIncome(IncomeCreate model)
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

        public IEnumerable<IncomeListItem> GetIncome()
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
    }
}
