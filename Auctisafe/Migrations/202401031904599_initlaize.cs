namespace Auctisafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initlaize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPaymentCredentials",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IBAN = c.String(),
                        Accountname = c.String(),
                        Accountnumber = c.String(),
                        Bank = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserPaymentCredentials");
        }
    }
}
