namespace BudgetGuru.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        BillDescription = c.String(),
                        MonthlyCost = c.Double(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BillId);
            
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        BudgetId = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 128),
                        MonthId = c.Int(nullable: false),
                        Income = c.Double(nullable: false),
                        MonthTotalCost = c.Double(nullable: false),
                        MonthlyExpenditures = c.Double(nullable: false),
                        ExtraExpenditures = c.Double(nullable: false),
                        Bills = c.Double(nullable: false),
                        Debt = c.Double(nullable: false),
                        NetProfit = c.Double(nullable: false),
                        Savings = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.BudgetId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserName)
                .Index(t => t.UserName);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Debts",
                c => new
                    {
                        DebtId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Description = c.String(),
                        Interest = c.Double(nullable: false),
                        DebtValue = c.Double(nullable: false),
                        AmountPaid = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.DebtId);
            
            CreateTable(
                "dbo.Expenditures",
                c => new
                    {
                        ExpenditureId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        ExpenditureDescription = c.String(),
                        ExpenditureCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ExpenditureId);
            
            CreateTable(
                "dbo.Goals",
                c => new
                    {
                        GoalId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        GoalDescription = c.String(),
                        AchievementDate = c.DateTime(),
                        TotalCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.GoalId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Budgets", "UserName", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Budgets", new[] { "UserName" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Goals");
            DropTable("dbo.Expenditures");
            DropTable("dbo.Debts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Budgets");
            DropTable("dbo.Bills");
        }
    }
}
