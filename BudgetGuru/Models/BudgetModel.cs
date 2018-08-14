using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetGuru.Models
{
    public class BudgetModel
    {
        public Budget Budget { get; set; }
        public Bills Bills { get; set; }
        public Debt Debt { get; set; }
        public Expenditures Expenditures { get; set; }
        public Goals Goals { get; set; }

        public List<Budget> BudgetList { get; set; }
        public List<Bills> BillsList { get; set; }
        public List<Debt> DebtList { get; set; }
        public List<Expenditures> ExpendituresList { get; set; }
        public List<Goals> GoalsList { get; set; }
    }
}