namespace Auctisafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialize6 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Winners");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Winners",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
