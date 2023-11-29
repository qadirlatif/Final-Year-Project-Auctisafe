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
                        Date = c.DateTime(nullable: false),
                        Bid_ID = c.Int(nullable: false),
                        Product_ID = c.Int(nullable: false),
                        Bidder_ID = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Date);
            
            CreateTable(
                "dbo.Auction_fees",
                c => new
                    {
                        Bid_ID = c.Int(nullable: false, identity: true),
                        Auction_fee_rate = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Bid_ID);
            
            CreateTable(
                "dbo.Auction_Status",
                c => new
                    {
                        keyparam = c.String(nullable: false, maxLength: 128),
                        Product_ID = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.keyparam);
            
            CreateTable(
                "dbo.Auction_Types",
                c => new
                    {
                        Auction_type_ID = c.Int(nullable: false, identity: true),
                        Auction_name = c.String(),
                    })
                .PrimaryKey(t => t.Auction_type_ID);
            
            CreateTable(
                "dbo.Auctioneer_recieving_Amount",
                c => new
                    {
                        Bid_ID = c.Int(nullable: false, identity: true),
                        Final_Amount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Bid_ID);
            
            CreateTable(
                "dbo.auctions",
                c => new
                    {
                        keyparams = c.String(nullable: false, maxLength: 128),
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
                    })
                .PrimaryKey(t => t.keyparams);
            
            CreateTable(
                "dbo.Bid_winner",
                c => new
                    {
                        Date = c.DateTime(nullable: false),
                        Bid_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Date);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Category_ID = c.Int(nullable: false, identity: true),
                        Category_name = c.String(),
                    })
                .PrimaryKey(t => t.Category_ID);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Body = c.String(nullable: false, maxLength: 128),
                        ContactID = c.Int(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                        phone = c.String(),
                        Subject = c.String(),
                    })
                .PrimaryKey(t => t.Body);
            
            CreateTable(
                "dbo.User_Account",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        User_ID = c.Int(nullable: false),
                        Password = c.String(),
                        Status = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Email);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Bid_ID = c.Int(nullable: false, identity: true),
                        Bid_Amount = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Bid_ID);
            
            CreateTable(
                "dbo.ProductAuctionMasters",
                c => new
                    {
                        Product_ID = c.Int(nullable: false, identity: true),
                        User_ID = c.Int(nullable: false),
                        name = c.String(),
                        description = c.String(),
                        Category_ID = c.Int(nullable: false),
                        price = c.Int(nullable: false),
                        used = c.String(),
                        image = c.String(),
                        Auction_type_ID = c.Int(nullable: false),
                        Start_date = c.DateTime(nullable: false),
                        End_date = c.DateTime(nullable: false),
                        increament = c.Int(nullable: false),
                        decreament = c.Int(nullable: false),
                        auto_accept_amount = c.Int(nullable: false),
                        Status = c.String(),
                        username = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Product_ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        name = c.String(nullable: false, maxLength: 128),
                        Product_ID = c.Int(nullable: false),
                        User_ID = c.Int(nullable: false),
                        description = c.String(),
                        Category_ID = c.Int(nullable: false),
                        price = c.Int(nullable: false),
                        used = c.String(),
                        image = c.String(),
                    })
                .PrimaryKey(t => t.name);
            
            CreateTable(
                "dbo.Report",
                c => new
                    {
                        ReportID = c.String(nullable: false, maxLength: 128),
                        Reporter_ID = c.Int(nullable: false),
                        User_ID = c.Int(nullable: false),
                        Reason = c.String(),
                        description = c.String(),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReportID);
            
            CreateTable(
                "dbo.USER",
                c => new
                    {
                        First_name = c.String(nullable: false, maxLength: 128),
                        User_ID = c.Int(nullable: false),
                        Last_name = c.String(),
                        Phone_number = c.Int(nullable: false),
                        CNIC = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.First_name);
            
            CreateTable(
                "dbo.signupmasters",
                c => new
                    {
                        User_ID = c.Int(nullable: false, identity: true),
                        First_name = c.String(),
                        Last_name = c.String(),
                        Phone_number = c.Int(nullable: false),
                        CNIC = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.User_ID);
            
            CreateTable(
                "dbo.verifications",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        verificationcode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.verifications");
            DropTable("dbo.signupmasters");
            DropTable("dbo.USER");
            DropTable("dbo.Report");
            DropTable("dbo.Product");
            DropTable("dbo.ProductAuctionMasters");
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
