namespace Auctisafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agreements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        WinnerID = c.Int(nullable: false),
                        AuctioneerID = c.Int(nullable: false),
                        AgreementName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Agreements");
        }
    }
}
