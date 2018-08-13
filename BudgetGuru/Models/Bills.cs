using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetGuru.Models
{
    public class Bills
    {
        [Key]
        public int BillId { get; set; }
        public string UserName { get; set; }
        [Display(Name = "Bill Description")]
        public string BillDescription { get; set; }
        [Display(Name = "Monthly Cost")]
        public double MonthlyCost { get; set; }
        [Display(Name = "Paid?")]
        public bool IsPaid { get; set; }
    }
}