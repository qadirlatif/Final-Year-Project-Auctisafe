namespace Auctisafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payment", "DueDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payment", "DueDate");
        }
    }
}
