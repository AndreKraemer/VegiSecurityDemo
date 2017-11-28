namespace Ak.ElVegetarianoFurio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    Description = c.String(maxLength: 250),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Dishes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    Description = c.String(maxLength: 250),
                    Price = c.Double(nullable: false),
                    CategoryId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);

            CreateTable(
                "dbo.Coupons",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Date = c.DateTime(nullable: false),
                    Ammount = c.Int(nullable: false),
                    UserId = c.String(nullable: false, maxLength: 100),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DeliveryAddresses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    LastName = c.String(nullable: false, maxLength: 80),
                    FirstName = c.String(nullable: false, maxLength: 80),
                    Street = c.String(nullable: false, maxLength: 80),
                    Zip = c.String(nullable: false, maxLength: 8),
                    City = c.String(nullable: false, maxLength: 80),
                    UserId = c.String(nullable: false, maxLength: 100),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Invoices",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Filename = c.String(nullable: false, maxLength: 80),
                    InvoiceDate = c.DateTime(nullable: false),
                    Total = c.Double(nullable: false),
                    UserId = c.String(nullable: false, maxLength: 100),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.PaymentInfoes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CardType = c.Int(nullable: false),
                    AccountNumber = c.String(nullable: false, maxLength: 20),
                    ExpirationDate = c.String(nullable: false, maxLength: 8),
                    CCV = c.String(nullable: false, maxLength: 3),
                    Cardholder = c.String(nullable: false, maxLength: 100),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

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
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");


            CreateStoredProcedure("dbo.FindDishes", c => new
            {
                SearchTerm = c.String(50)
            }, @"
                  DECLARE @query VARCHAR(100)
                  SET @query = 'SELECT Id, Name, Description, Price, CategoryId FROM Dishes WHERE Name LIKE ''%' + @SearchTerm + '%'''
                  EXEC(@query)
                "
            );

        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PaymentInfoes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Dishes", "CategoryId", "dbo.Categories");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.PaymentInfoes", new[] { "UserId" });
            DropIndex("dbo.Dishes", new[] { "CategoryId" });
            DropStoredProcedure("dbo.FindDishes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.PaymentInfoes");
            DropTable("dbo.Invoices");
            DropTable("dbo.DeliveryAddresses");
            DropTable("dbo.Coupons");
            DropTable("dbo.Dishes");
            DropTable("dbo.Categories");
        }
    }
}
