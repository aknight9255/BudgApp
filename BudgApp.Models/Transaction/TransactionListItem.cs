using BudgApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgApp.Models.Transaction
{
    public class TransactionListItem
    {
        public int TransactionID { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public float TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
