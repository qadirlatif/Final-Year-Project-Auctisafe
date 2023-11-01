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
}