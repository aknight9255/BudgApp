using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgApp.Models.Income
{
    public class IncomeCreate
    {
        [Required]
        public float IncomeAmount { get; set; }
        [Required]
        public DateTime IncomeDate { get; set; }
    }
}
