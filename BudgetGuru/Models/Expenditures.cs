using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetGuru.Models
{
    public class Expenditures
    {
        [Key]
        public int ExpenditureId { get; set; }
        public string UserName { get; set; }
        [Display(Name = "Expenditure Description")]
        public string ExpenditureDescription { get; set; }
        [Display(Name = "Expenditure Cost")]
        public double ExpenditureCost { get; set; }
        [Display(Name = "Month")]
        public int ExpendMonth { get; set; }
        [Display(Name ="Year")]
        public int ExpendYear { get; set; }
    }
}