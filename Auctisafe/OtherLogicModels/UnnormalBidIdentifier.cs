using Auctisafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Auctisafe.OtherLogicModels
{
    public class UnnormalBidIdentifier
    {
        private static mailer emailService;

        private static Timer _timer;

        public static void Start()
        {
            emailService = new mailer();
            int interval = 5 * 60 * 1000;
            _timer = new Timer(TimerCallback, null, 0, interval);
        }
        public static void TimerCallback(object state) 
        {

            var db = new AuctionContext();
            var activeauctions = db.auction_status.Where(x => x.Status == "A").ToList();
            foreach(var active in activeauctions)
            {
                var auction = db.auctions.Where(x => x.Product_ID == active.Product_ID).FirstOrDefault();
                var product = db.Products.Where(x => x.Product_ID == active.Product_ID).FirstOrDefault();
                var biddings = db.all_biddings.Where(x => x.Product_ID == active.Product_ID).OrderBy(x=>x.Amount).ToList();
               if(auction.Auction_type_ID == 1)
                {
                    EnglishAuctionbids(db, product, auction, biddings);
                }
               else if(auction.Auction_type_ID == 3 || auction.Auction_type_ID == 6)
                {
                    SealedBid_ForwardAuctionBids(db, product, auction, biddings);
                }
               else if(auction.Auction_type_ID == 4)
                {
                    ReverseAuctionBids(db, product, auction, biddings);
                }
               else if(auction.Auction_type_ID == 5)
                {
                    ReserveAuctionBids(db, product, auction, biddings);
                }
            }
        }
        public static void EnglishAuctionbids(AuctionContext db, Product product, Auctions auction, List<biddings> bids)
        {
            int index = 0;
            double extraamount = 0;
            double previousbid = 0;
            foreach(var bid in bids)
            {
                if(index == 0)
                {
                    if(bid.Amount > (1.1 * product.price))
                    {
                        db.all_biddings.Remove(bid);
                        db.SaveChanges();
                        

                    }
                    else
                    {
                        previousbid = bid.Amount;
                    }
                    ++index;
                }
                else
                {
                    var calculatedamount = bid.Amount - extraamount;
                    if(calculatedamount > (1.1 * previousbid))
                    {
                        db.all_biddings.Remove(bid);
                        db.SaveChanges();
                        extraamount = extraamount + (bid.Amount - previousbid - extraamount);
                        //previousbid = bid.Amount;
                    }
                    else
                    {
                        bid.Amount = calculatedamount;
                        db.Entry(bid).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        previousbid = bid.Amount;
                    }
                }
            }

        }
        public static void SealedBid_ForwardAuctionBids(AuctionContext db, Product product, Auctions auction, List<biddings> bids)
        {
            foreach(var bid in bids)
            {
                if(bid.Amount >(1.1 * product.price))
                {
                    db.all_biddings.Remove(bid);
                    db.SaveChanges();
                }
            }
        }
        public static void ReverseAuctionBids(AuctionContext db, Product product, Auctions auction, List<biddings> bids)
        {
            foreach(var bid in bids)
            {
                if(bid.Amount < (0.9 * product.price))
                {
                    db.all_biddings.Remove(bid);
                    db.SaveChanges();
                }
            }
        }
        public static void ReserveAuctionBids (AuctionContext db, Product product, Auctions auction, List<biddings> bids)
        {
            foreach(var bid in bids)
            {
                if(bid.Amount > (1.1 * auction.auto_accept_amount))
                {
                    db.all_biddings.Remove(bid);
                    db.SaveChanges();
                }
            }
        }
    }
}