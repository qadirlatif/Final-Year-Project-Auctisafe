namespace Auctisafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auction_fees", "productID", c => c.Int(nullable: false));
            AddColumn("dbo.Auction_fees", "Auctionfees", c => c.Single(nullable: false));
            AddColumn("dbo.Auction_fees", "WinnerID", c => c.Int(nullable: false));
            AddColumn("dbo.Auction_fees", "AuctioneerID", c => c.Int(nullable: false));
            AddColumn("dbo.Auctioneer_recieving_Amount", "productID", c => c.Int(nullable: false));
            AddColumn("dbo.Auctioneer_recieving_Amount", "TotalAmount", c => c.Single(nullable: false));
            AddColumn("dbo.Auctioneer_recieving_Amount", "WinnerID", c => c.Int(nullable: false));
            AddColumn("dbo.Auctioneer_recieving_Amount", "AuctioneerID", c => c.Int(nullable: false));
            AddColumn("dbo.Tax_on_Auction", "productID", c => c.Int(nullable: false));
            AddColumn("dbo.Tax_on_Auction", "TaxAmount", c => c.Single(nullable: false));
            AddColumn("dbo.Tax_on_Auction", "WinnerID", c => c.Int(nullable: false));
            AddColumn("dbo.Tax_on_Auction", "AuctioneerID", c => c.Int(nullable: false));
            DropColumn("dbo.Auction_fees", "Bid_ID");
            DropColumn("dbo.Auction_fees", "Auction_fee_rate");
            DropColumn("dbo.Auctioneer_recieving_Amount", "Bid_ID");
            DropColumn("dbo.Auctioneer_recieving_Amount", "Final_Amount");
            DropColumn("dbo.Tax_on_Auction", "Bid_ID");
            DropColumn("dbo.Tax_on_Auction", "Tax_rate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tax_on_Auction", "Tax_rate", c => c.Int(nullable: false));
            AddColumn("dbo.Tax_on_Auction", "Bid_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Auctioneer_recieving_Amount", "Final_Amount", c => c.Single(nullable: false));
            AddColumn("dbo.Auctioneer_recieving_Amount", "Bid_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Auction_fees", "Auction_fee_rate", c => c.Int(nullable: false));
            AddColumn("dbo.Auction_fees", "Bid_ID", c => c.Int(nullable: false));
            DropColumn("dbo.Tax_on_Auction", "AuctioneerID");
            DropColumn("dbo.Tax_on_Auction", "WinnerID");
            DropColumn("dbo.Tax_on_Auction", "TaxAmount");
            DropColumn("dbo.Tax_on_Auction", "productID");
            DropColumn("dbo.Auctioneer_recieving_Amount", "AuctioneerID");
            DropColumn("dbo.Auctioneer_recieving_Amount", "WinnerID");
            DropColumn("dbo.Auctioneer_recieving_Amount", "TotalAmount");
            DropColumn("dbo.Auctioneer_recieving_Amount", "productID");
            DropColumn("dbo.Auction_fees", "AuctioneerID");
            DropColumn("dbo.Auction_fees", "WinnerID");
            DropColumn("dbo.Auction_fees", "Auctionfees");
            DropColumn("dbo.Auction_fees", "productID");
        }
    }
}
