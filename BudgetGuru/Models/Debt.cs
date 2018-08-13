using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetGuru.Models
{
    public class Debt
    {
        [Key]
        public int DebtId { get; set; }
        public string UserName { get; set; }
        [Display(Name = "Debt Description")]
        public string Description { get; set; }
        [Display(Name = "Interest Rate")]
        public double Interest { get; set; }
        [Display(Name = "Remaining Debt")]
        public double DebtValue { get; set; }
        [Display(Name = "Total Paid")]
        public double AmountPaid { get; set; }
    }
}