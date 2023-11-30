namespace Auctisafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Biddings");
            AddColumn("dbo.Biddings", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Biddings", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Biddings");
            DropColumn("dbo.Biddings", "ID");
            AddPrimaryKey("dbo.Biddings", "Date");
        }
    }
}
