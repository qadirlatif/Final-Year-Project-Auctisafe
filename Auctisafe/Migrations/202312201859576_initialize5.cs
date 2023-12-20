namespace Auctisafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.transaction",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TransactionID = c.String(),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.transaction");
        }
    }
}
