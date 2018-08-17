namespace BudgetGuru.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class monthlylimitadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Budgets", "MonthlyLimit", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Budgets", "MonthlyLimit");
        }
    }
}
