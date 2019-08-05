using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgApp.Models.Category
{
    public class CategoryDetails
    {
        public int CategoryID { get; set; }
        [Display(Name = "Category")]
        public string CategoryType { get; set; }
    }
}
