namespace Auctisafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.transaction", "TransactionImage", c => c.String());
            DropColumn("dbo.transaction", "TransactionID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.transaction", "TransactionID", c => c.String());
            DropColumn("dbo.transaction", "TransactionImage");
        }
    }
}
