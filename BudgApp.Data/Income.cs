using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgApp.Data
{
    public class Income
    {
        [Key]
        public int IncomeID { get; set; }
        [Required]
        [Display(Name = "Amount")]
        public float IncomeAmount { get; set; }
        [Required]
        [Display(Name = "Date")]
        public DateTime IncomeDate { get; set; }

        public Guid AccountID { get; set; }
    }
}
