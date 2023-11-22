using Auctisafe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;

namespace Auctisafe.OtherLogicModels
{
    public class AuctionTimerObserver
    {
        private static Timer _timer;

        public static void Start()
        {
            // Set the interval to 15 minutes (in milliseconds)
            int interval = 1 * 60 * 1000;

            // Create a timer that calls the TimerCallback method once every 15 minutes
            _timer = new Timer(TimerCallback, null, 0, interval);
        }

        private static void TimerCallback(object state)
        {
            AuctionContext context = new AuctionContext();
            var products = context.Products.ToList();
            foreach (var product in products)
            {
                var allauctions = context.auctions.ToList();
                var auction = context.auctions.FirstOrDefault(x => x.Product_ID == product.Product_ID);
                DutchAuctionTimer(context, product, auction);
            }
        }


        public static void DutchAuctionTimer(AuctionContext context, Product product, Auctions auction)
        {


            if (auction != null && auction.Auction_type_ID == 2)
            {
                if (auction.End_date > DateTime.Now)
                {
                    var auctionstatus = context.auction_status.Where(x => x.Product_ID == product.Product_ID).FirstOrDefault();
                    if (auctionstatus.Status == "A")
                    {
                        var auctionStartTime = auction.currentdate;

                        var timeIntervalInMinutes = auction.IntervalinHours;
                        var decrementAmount = auction.decreament;

                        var currentTime = DateTime.Now;

                        if ((currentTime - auctionStartTime).TotalMinutes >= timeIntervalInMinutes)
                        {
                            auction.currentdate = currentTime;

                            auction.UpdatedPrice -= decrementAmount;
                        }
                        context.Entry(auction).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
        }

        public static void Stop()
        {
            // Stop the timer when it's no longer needed
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}