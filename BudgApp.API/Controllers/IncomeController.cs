using BudgApp.Models.Income;
using BudgApp.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BudgApp.API.Controllers
{
    [Authorize]
    public class IncomeController : ApiController
    {
        // GET: Income
        public IHttpActionResult GetAll()
        {
            IncomeService incomeService = CreateIncomeService();
            var incomes = incomeService.GetIncome();
            return Ok(incomes);
        }

        public IHttpActionResult Get(int id)
        {
            IncomeService incomeService = CreateIncomeService();
            var incomes = incomeService.GetIncomeByID(id);
            return Ok(incomes);

        }

        public IHttpActionResult Get(DateTime coolDate)
        {
            IncomeService incomeService = CreateIncomeService();
            var incomes = incomeService.GetIncomeByMonth(coolDate);
            return Ok(incomes);

        }

        public IHttpActionResult Post(IncomeCreate income)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateIncomeService();

            if (!service.CreateIncome(income))
                return InternalServerError();

            return Ok();

        }

        public IHttpActionResult Put(IncomeEdit income)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateIncomeService();

            if (!service.UpdateIncome(income))
                return InternalServerError();

            return Ok();

        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateIncomeService();

            if (!service.DeleteIncome(id))
                return InternalServerError();

            return Ok();
        }

        private IncomeService CreateIncomeService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var incomeService = new IncomeService(userID);
            return incomeService;
        }
    }
}