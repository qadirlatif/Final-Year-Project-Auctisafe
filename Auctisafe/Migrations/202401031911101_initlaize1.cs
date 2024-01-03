namespace Auctisafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initlaize1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPaymentCredentials", "UserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPaymentCredentials", "UserID");
        }
    }
}
