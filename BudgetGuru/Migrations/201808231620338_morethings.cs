namespace BudgetGuru.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class morethings : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Budgets", "MonthTotalCost", c => c.Double());
            AlterColumn("dbo.Budgets", "MonthlyExpenditures", c => c.Double());
            AlterColumn("dbo.Budgets", "Bills", c => c.Double());
            AlterColumn("dbo.Budgets", "NetProfit", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Budgets", "NetProfit", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "Bills", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "MonthlyExpenditures", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "MonthTotalCost", c => c.Double(nullable: false));
        }
    }
}
