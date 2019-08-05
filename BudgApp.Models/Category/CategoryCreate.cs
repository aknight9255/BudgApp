using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BudgApp.Models
{
     public class CategoryCreate
    {
        [Required]
        public string CategoryType { get; set; }
    }
}
