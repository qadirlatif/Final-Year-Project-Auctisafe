using Auctisafe.Models;
using Auctisafe.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Web;

namespace Auctisafe.OtherLogicModels
{
    public class AutomationBidWinnerDetector
    {
        private static mailer emailService;

        private static Timer _timer;

        public static void Start()
        {
            emailService = new mailer();
            int interval = 15 * 60 * 1000;
            _timer = new Timer(TimerCallback, null, 0, interval);
        }

        private static void TimerCallback(object state)
        {
            AuctionContext context = new AuctionContext();
            var allauctionstatus = context.auction_status.Where(x => x.Status == "A").ToList();
            //var allauctions = context.auctions.Where(x=>x.End_date < DateTime.Now).ToList();
            //var products = context.Products.ToList();
            foreach (var targetauction in allauctionstatus)
            {
                
                var auction = context.auctions.FirstOrDefault(x => x.Product_ID == targetauction.Product_ID && x.End_date < DateTime.Now);
                if (auction != null)
                {
                    var product = context.Products.Where(x => x.Product_ID == auction.Product_ID).FirstOrDefault();
                    //if (auction.End_date < DateTime.Now)
                    //{
                        if (auction.Auction_type_ID == 1 || auction.Auction_type_ID == 3 || auction.Auction_type_ID == 6)
                        {
                            //var status = context.auction_status.FirstOrDefault(x => x.Product_ID == auction.Product_ID);

                            //if (status.Status == "A")
                            //{

                                English_SealedBid_Auction_Bidwinner(context, product);

                            //}
                        }
                        else if (auction.Auction_type_ID == 2)
                        {
                            //var status = context.auction_status.FirstOrDefault(x => x.Product_ID == auction.Product_ID);
                            //if (status.Status == "A")
                            //{
                                DutchAuction(context, product);
                            //}


                        }
                        else if (auction.Auction_type_ID == 4)
                        {
                            //var status = context.auction_status.FirstOrDefault(x => x.Product_ID == auction.Product_ID);
                            //if (status.Status == "A")
                            //{
                                ReverseAuction_BidWinner(context, product);
                            //}
                        }
                        else if (auction.Auction_type_ID == 5)
                        {
                            //var status = context.auction_status.FirstOrDefault(x => x.Product_ID == auction.Product_ID);
                            //if (status.Status == "A")
                            //{
                                ReserveAuction(context, product);
                            //}
                        }
                    //}
                }
            }


        }

        public static void DutchAuction(AuctionContext context, Product product)
        {
            var bidders = new List<BidAndBidder>();
            var biddings = context.all_biddings.Where(x => x.Product_ID == product.Product_ID).ToList();
            var auction = context.auctions.Where(x => x.Product_ID == product.Product_ID).FirstOrDefault();
            var bidder = new Signup();

            double initialBidAmount = product.price;
            double decrementAmount = auction.decreament;

            foreach (var bids in biddings)
            {
                var bidderdetails = context.signup.Where(x => x.User_ID == bids.Bidder_ID).FirstOrDefault();
                var credential = context.login.Where(x => x.User_ID == bids.Bidder_ID).FirstOrDefault();
                bidders.Add(new BidAndBidder { bidding = bids, bidder = bidderdetails, credentails = credential });
            }

            var winner = new BidAndBidder();
            double currentBidAmount = auction.UpdatedPrice;

            foreach (var biddingandbidders in bidders)
            {
                if (biddingandbidders.bidding.Amount >= currentBidAmount)
                {
                    winner.bidder = biddingandbidders.bidder;
                    winner.bidding = biddingandbidders.bidding;
                    winner.credentails = biddingandbidders.credentails;
                    currentBidAmount = biddingandbidders.bidding.Amount;
                }

            }

            var target = context.auction_status.Where(x => x.Product_ID == product.Product_ID).FirstOrDefault();
            target.Status = "D"; // You might want to update the status based on your requirements
            context.Entry(target).State = EntityState.Modified;
            context.SaveChanges();

            var bidwinner = new bid_winner();

            if (winner != null)
            {
                bidwinner.Bid_ID = winner.bidding.Bid_ID;
                bidwinner.Date = DateTime.Now;
                context.bid_winner.Add(bidwinner);
                context.SaveChanges();

                if (winner != null)
                {
                    emailService.Emailer(winner.credentails.Email, "Congratulations! You Won the Dutch Auction", "Dear " + winner.bidder.First_name + " " + winner.bidder.Last_name + ", you won the Dutch auction for the item named: " + product.name);
                }
            }

            if (winner != null)
            {
                paymentgenerator(context, winner, product);  
            }


        }
        public static void paymentgenerator(AuctionContext context, BidAndBidder winner, Product product)
        {
            payment pendingpayment = new payment();
            pendingpayment.WinnerID = winner.bidding.Bidder_ID;
            pendingpayment.date = DateTime.Now;
            pendingpayment.ProductID = product.Product_ID;
            pendingpayment.AuctioneerID = product.User_ID;
            pendingpayment.DueDate = pendingpayment.date.AddDays(3);
            pendingpayment.Bid_Amount = (float)(winner.bidding.Amount);
            context.Payment.Add(pendingpayment);
            context.SaveChanges();
            var auctionfee = pendingpayment.Bid_Amount * 0.05;
            var tax = pendingpayment.Bid_Amount * 0.1;
            auction_fees fee = new auction_fees();
            fee.productID = product.Product_ID;
            fee.AuctioneerID = product.User_ID;
            fee.WinnerID = winner.bidding.Bidder_ID;
            fee.Auctionfees = (float)(auctionfee);
            fee.date = DateTime.Now;
            context.auction_fees.Add(fee);
            context.SaveChanges();
            Tax auctax = new Tax();
            auctax.productID = product.Product_ID;
            auctax.WinnerID = winner.bidder.User_ID;
            auctax.AuctioneerID = product.User_ID;
            auctax.TaxAmount = (float)tax;
            auctax.Date = DateTime.Now;
            context.Tax.Add(auctax);
            context.SaveChanges();
            auctioneer_receiving_amount receive = new auctioneer_receiving_amount();
            receive.AuctioneerID = product.User_ID;
            receive.productID = product.Product_ID;
            receive.WinnerID = winner.bidder.User_ID;
            receive.TotalAmount = (float)(winner.bidding.Amount - auctionfee - tax);
            context.auctioneer_recieving_amount.Add(receive);
            context.SaveChanges();
        }


        public static void ReverseAuction_BidWinner(AuctionContext context, Product product)
        {
            var bidders = new List<BidAndBidder>();
            var biddings = context.all_biddings.Where(x => x.Product_ID == product.Product_ID).ToList();
            var bidder = new Signup();

            foreach (var bids in biddings)
            {
                var bidderdetails = context.signup.Where(x => x.User_ID == bids.Bidder_ID).FirstOrDefault();
                var credential = context.login.Where(x => x.User_ID == bids.Bidder_ID).FirstOrDefault();

                //var initialBidAmount = product.price; 
                //bids.Amount = initialBidAmount;

                bidders.Add(new BidAndBidder { bidding = bids, bidder = bidderdetails, credentails = credential });
            }

            var winner = new BidAndBidder();
            double amount = double.MaxValue;

            foreach (var biddingandbidders in bidders)
            {
                if (biddingandbidders.bidding.Amount < amount)
                {
                    winner.bidder = biddingandbidders.bidder;
                    winner.bidding = biddingandbidders.bidding;
                    winner.credentails = biddingandbidders.credentails;
                    amount = biddingandbidders.bidding.Amount;
                }
            }

            var target = context.auction_status.Where(x => x.Product_ID == product.Product_ID).FirstOrDefault();
            target.Status = "D";
            context.Entry(target).State = EntityState.Modified;
            context.SaveChanges();

            var bidwinner = new bid_winner();

            if (winner != null)
            {
                bidwinner.Bid_ID = winner.bidding.Bid_ID;
                bidwinner.Date = DateTime.Now;
                context.bid_winner.Add(bidwinner);
                context.SaveChanges();

                if (winner != null)
                {
                    emailService.Emailer(winner.credentails.Email, "Congratulations! You Won the Reverse Auction", "Dear " + winner.bidder.First_name + " " + winner.bidder.Last_name + " you won the reverse auction for the item named: " + product.name);
                }
            }
            if (winner != null)
            {
                paymentgenerator(context, winner, product);
            }
        }


        public static void English_SealedBid_Auction_Bidwinner(AuctionContext context, Product product)
        {
            var bidders = new List<BidAndBidder>();
            var biddings = context.all_biddings.Where(x => x.Product_ID == product.Product_ID).ToList();
            var bidder = new Signup();
            foreach (var bids in biddings)
            {
                var bidderdetails = context.signup.Where(x => x.User_ID == bids.Bidder_ID).FirstOrDefault();
                var credential = context.login.Where(x => x.User_ID == bids.Bidder_ID).FirstOrDefault();
                bidders.Add(new BidAndBidder { bidding = bids, bidder = bidderdetails, credentails = credential });
            }
            var winner = new BidAndBidder();
            double amount = 0;
            foreach (var biddingandbidders in bidders)
            {
                if (biddingandbidders.bidding.Amount > amount)
                {
                    winner.bidder = biddingandbidders.bidder;
                    winner.bidding = biddingandbidders.bidding;
                    winner.credentails = biddingandbidders.credentails;
                    amount = biddingandbidders.bidding.Amount;
                }
            }
            var target = context.auction_status.Where(x => x.Product_ID == product.Product_ID).FirstOrDefault();
            target.Status = "D";
            context.SaveChanges();
            var bidwinner = new bid_winner();
            if (winner != null)
            {


                bidwinner.Bid_ID = winner.bidding.Bid_ID;
                bidwinner.Date = DateTime.Now;
                context.bid_winner.Add(bidwinner);
                context.SaveChanges();
                if (winner != null)
                {
                    emailService.Emailer(winner.credentails.Email, "Congratulations! You Wins the BID", "Dear " + winner.bidder.First_name + " " + winner.bidder.Last_name + " you wins the bid on item named: " + product.name);
                }
            }
            if(winner != null)
            {
                paymentgenerator(context, winner, product);

            }
        }
        public static void ReserveAuction(AuctionContext context, Product product)
        {
            var bidders = new List<BidAndBidder>();
            var biddings = context.all_biddings.Where(x => x.Product_ID == product.Product_ID).ToList();
            var auction = context.auctions.Where(x => x.Product_ID == product.Product_ID).FirstOrDefault();
            var bidder = new Signup();

            double initialBidAmount = product.price;
            double reservePrice = auction.auto_accept_amount;

            foreach (var bids in biddings)
            {
                var bidderdetails = context.signup.Where(x => x.User_ID == bids.Bidder_ID).FirstOrDefault();
                var credential = context.login.Where(x => x.User_ID == bids.Bidder_ID).FirstOrDefault();
                bidders.Add(new BidAndBidder { bidding = bids, bidder = bidderdetails, credentails = credential });
            }

            var winner = new BidAndBidder();
            double currentBidAmount = reservePrice;

            foreach (var biddingandbidders in bidders)
            {
                if (biddingandbidders.bidding.Amount >= reservePrice && biddingandbidders.bidding.Amount >= currentBidAmount)
                {
                    winner.bidder = biddingandbidders.bidder;
                    winner.bidding = biddingandbidders.bidding;
                    winner.credentails = biddingandbidders.credentails;
                    currentBidAmount = biddingandbidders.bidding.Amount;
                }
            }

            if (currentBidAmount >= reservePrice)
            {
                var target = context.auction_status.Where(x => x.Product_ID == product.Product_ID).FirstOrDefault();
                target.Status = "D";
                context.Entry(target).State = EntityState.Modified;
                context.SaveChanges();

                var bidwinner = new bid_winner();

                if (bidwinner != null)
                {
                    bidwinner.Bid_ID = winner.bidding.Bid_ID;
                    bidwinner.Date = DateTime.Now;
                    context.bid_winner.Add(bidwinner);
                    context.SaveChanges();

                    if (winner != null)
                    {
                        emailService.Emailer(winner.credentails.Email, "Congratulations! You Won the Reserve Auction", "Dear " + winner.bidder.First_name + " " + winner.bidder.Last_name + ", you won the Reserve auction for the item named: " + product.name);
                    }
                }
                if (winner != null)
                {
                    paymentgenerator(context, winner, product);
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