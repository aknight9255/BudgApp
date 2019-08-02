using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgApp.Data
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }
        public Guid AccountID { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public float TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
