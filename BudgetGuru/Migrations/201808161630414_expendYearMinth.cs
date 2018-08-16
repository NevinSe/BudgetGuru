namespace BudgetGuru.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expendYearMinth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenditures", "ExpendMonth", c => c.Int(nullable: false));
            AddColumn("dbo.Expenditures", "ExpendYear", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Expenditures", "ExpendYear");
            DropColumn("dbo.Expenditures", "ExpendMonth");
        }
    }
}
