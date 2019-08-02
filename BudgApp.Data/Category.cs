using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgApp.Data
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryType { get; set; }
    }
}
