using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    public class AuctionContext : DbContext
    {
        public DbSet<Signup> signup { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Login> login { get; set; }
        public DbSet<auction_fees> auction_fees { get; set; }
        public DbSet<Auction_status> auction_status { get; set; } 
        public DbSet<Auction_types> auction_Types { get; set; }
        public DbSet<auctioneer_receiving_amount> auctioneer_recieving_amount { get; set; }
        public DbSet<Auctions> auctions { get; set; }
        public DbSet<bid_winner> bid_winner { get; set; }
        public DbSet<biddings> all_biddings { get; set; }
        public DbSet<product_categories> categories { get; set; }
        public DbSet<payment> Payment { get; set; }
        public DbSet<report> Reports { get; set; }



        public System.Data.Entity.DbSet<Auctisafe.Models.signupmaster> tests { get; set; }
        public System.Data.Entity.DbSet<Auctisafe.Models.verification> verify { get; set; }

        public System.Data.Entity.DbSet<Auctisafe.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<Auctisafe.Models.ProductAuctionMaster> ProductAuctionMasters { get; set; }
    }
}