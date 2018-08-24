using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BudgetGuru.Models
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Month")]
        public int MonthId { get; set; }
        [Display(Name = "Year")]
        public int Year { get; set; }
        [Display(Name = "Income")]
        public double Income { get; set; }
        [Display(Name = "Montly Limit")]
        public double MonthlyLimit { get; set; }
        [Display(Name = "Monthly Cost")]
        public double? MonthTotalCost { get; set; }
        [Display(Name = "Monthly Expenditures")]
        public double? MonthlyExpenditures { get; set; }
        [Display(Name = "Extra Expenditures")]
        public double ExtraExpenditures { get; set; }
        [Display(Name = "Bills to Pay")]
        public double? Bills { get; set; }
        [Display(Name = "Debt Total")]
        public double Debt { get; set; }
        [Display(Name = "Net Profit")]
        public double? NetProfit { get; set; }
        [Display(Name = "Savings")]
        public double Savings { get; set; }

    }
}