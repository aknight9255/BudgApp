using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgApp.Models
{
    public class CategoryListItem
    {
        [Key]
        public int CategoryID { get; set; }
        
        [Display(Name = "Category")]
        public string CategoryType { get; set; }
    }
}
