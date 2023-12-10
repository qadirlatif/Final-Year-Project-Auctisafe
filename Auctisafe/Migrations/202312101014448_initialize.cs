namespace Auctisafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Biddings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Bid_ID = c.Int(nullable: false),
                        Product_ID = c.Int(nullable: false),
                        Bidder_ID = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Auction_fees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Bid_ID = c.Int(nullable: false),
                        Auction_fee_rate = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Auction_Status",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Product_ID = c.Int(nullable: false),
                        Status = c.String(),
                        keyparam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Auction_Types",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Auction_type_ID = c.Int(nullable: false),
                        Auction_name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Auctioneer_recieving_Amount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Bid_ID = c.Int(nullable: false),
                        Final_Amount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.auctions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Product_ID = c.Int(nullable: false),
                        Auction_type_ID = c.Int(nullable: false),
                        Start_date = c.DateTime(nullable: false),
                        End_date = c.DateTime(nullable: false),
                        increament = c.Int(nullable: false),
                        decreament = c.Int(nullable: false),
                        auto_accept_amount = c.Int(nullable: false),
                        IntervalinHours = c.Int(nullable: false),
                        UpdatedPrice = c.Int(nullable: false),
                        currentdate = c.DateTime(nullable: false),
                        keyparams = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Bid_winner",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Bid_ID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Category_ID = c.Int(nullable: false),
                        Category_name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ContactID = c.Int(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                        phone = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.User_Account",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        User_ID = c.Int(nullable: false),
                        Email = c.String(),
                        Password = c.String(),
                        Status = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Bid_ID = c.Int(nullable: false),
                        Bid_Amount = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Product_ID = c.Int(nullable: false),
                        User_ID = c.Int(nullable: false),
                        name = c.String(),
                        description = c.String(),
                        Category_ID = c.Int(nullable: false),
                        price = c.Int(nullable: false),
                        used = c.String(),
                        image = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Report",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Reporter_ID = c.Int(nullable: false),
                        User_ID = c.Int(nullable: false),
                        Reason = c.String(),
                        description = c.String(),
                        date = c.DateTime(nullable: false),
                        ReportID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.USER",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        User_ID = c.Int(nullable: false),
                        First_name = c.String(),
                        Last_name = c.String(),
                        Phone_number = c.Int(nullable: false),
                        CNIC = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tax_on_Auction",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Bid_ID = c.Int(nullable: false),
                        Tax_rate = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tax_on_Auction");
            DropTable("dbo.USER");
            DropTable("dbo.Report");
            DropTable("dbo.Product");
            DropTable("dbo.Payment");
            DropTable("dbo.User_Account");
            DropTable("dbo.Contact");
            DropTable("dbo.Categories");
            DropTable("dbo.Bid_winner");
            DropTable("dbo.auctions");
            DropTable("dbo.Auctioneer_recieving_Amount");
            DropTable("dbo.Auction_Types");
            DropTable("dbo.Auction_Status");
            DropTable("dbo.Auction_fees");
            DropTable("dbo.Biddings");
        }
    }
}
