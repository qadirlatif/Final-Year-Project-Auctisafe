namespace Auctisafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payment", "ProductID", c => c.Int(nullable: false));
            AddColumn("dbo.Payment", "WinnerID", c => c.Int(nullable: false));
            AddColumn("dbo.Payment", "AuctioneerID", c => c.Int(nullable: false));
            DropColumn("dbo.Payment", "Bid_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payment", "Bid_ID", c => c.Int(nullable: false));
            DropColumn("dbo.Payment", "AuctioneerID");
            DropColumn("dbo.Payment", "WinnerID");
            DropColumn("dbo.Payment", "ProductID");
        }
    }
}
