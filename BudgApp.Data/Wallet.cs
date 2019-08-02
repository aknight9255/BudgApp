using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgApp.Data
{
    public class Wallet
    {
        [Key]
        public int WalletID { get; set; }
        public Guid AccountID { get; set; }
        public float Balance { get; set; }

        //[ForeignKey]
        //public int TransactionID { get; set; }
        //public int MyProperty { get; set; }

    }
}
