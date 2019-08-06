using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgApp.Models.Income
{
    public class IncomeEdit
    {
        public  int IncomeID { get; set; }
        public float IncomeAmount{ get; set; }
        public DateTime IncomeDate{ get; set; }
    }
}
