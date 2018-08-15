namespace BudgetGuru.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ChartModel : DbContext
    {
        public ChartModel()
            : base("name=ChartModel")
        {
        }

        public virtual DbSet<Budget> Budgets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
