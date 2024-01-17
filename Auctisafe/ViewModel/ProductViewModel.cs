using Auctisafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auctisafe.ViewModel
{
    public class ProductViewModel
    {
    }
    public class ProductDetailsViewModel
    {
        public Signup AuctioneerDetail { get; set; }
        public Product product { get; set; }
        public Signup userdetails { get; set; }
        public Auction_status status { get; set; }
        public Auctions auction { get; set; }
        public List<BidAndBidder> biddingdetails { get; set; }
    }
    public class BidAndBidder
    {
        public biddings bidding { get; set; }
        public Signup bidder { get; set; }
        public Login credentails { get; set; }
    }
    public class AuctionListViewModel
    {
        public int  AuctionID { get; set; }
        public string Auctioneer_Name { get; set; }
        public string ProductName { get; set; }
        public int userid { get; set; }
        public string status { get; set; }
        public string Auction { get; set; }
    }
    public class producttrackerviewmodel
    {
        public string Name { get; set; }
        public int productid { get; set; }
        public string Status { get; set; }
    }
}