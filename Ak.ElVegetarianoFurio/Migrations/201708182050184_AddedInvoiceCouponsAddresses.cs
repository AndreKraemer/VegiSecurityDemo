namespace Ak.ElVegetarianoFurio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedInvoiceCouponsAddresses : DbMigration
    {
        public override void Up()
        {
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
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Invoices");
            DropTable("dbo.DeliveryAddresses");
            DropTable("dbo.Coupons");
        }
    }
}
