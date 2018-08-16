namespace BudgetGuru.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yearsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Budgets", "Year", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Budgets", "Year");
        }
    }
}
