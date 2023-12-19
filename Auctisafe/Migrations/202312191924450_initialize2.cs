namespace Auctisafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payment", "Bid_Amount", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payment", "Bid_Amount", c => c.Int(nullable: false));
        }
    }
}
