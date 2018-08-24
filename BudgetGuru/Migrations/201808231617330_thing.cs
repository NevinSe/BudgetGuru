namespace BudgetGuru.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thing : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bills", "MonthlyCost", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bills", "MonthlyCost", c => c.Double(nullable: false));
        }
    }
}
