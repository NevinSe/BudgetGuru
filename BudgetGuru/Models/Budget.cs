namespace BudgetGuru.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Budget
    {
        public int BudgetId { get; set; }

        public string UserName { get; set; }

        public int MonthId { get; set; }

        public double Income { get; set; }

        public double MonthTotalCost { get; set; }

        public double MonthlyExpenditures { get; set; }

        public double ExtraExpenditures { get; set; }

        public double Bills { get; set; }

        public double Debt { get; set; }

        public double NetProfit { get; set; }

        public double Savings { get; set; }
    }
}
