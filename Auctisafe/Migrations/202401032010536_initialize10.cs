namespace Auctisafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agreements", "UserID", c => c.Int(nullable: false));
            DropColumn("dbo.Agreements", "WinnerID");
            DropColumn("dbo.Agreements", "AuctioneerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Agreements", "AuctioneerID", c => c.Int(nullable: false));
            AddColumn("dbo.Agreements", "WinnerID", c => c.Int(nullable: false));
            DropColumn("dbo.Agreements", "UserID");
        }
    }
}
