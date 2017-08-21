namespace Ak.ElVegetarianoFurio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserColumnToInvoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "UserId", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "UserId");
        }
    }
}
