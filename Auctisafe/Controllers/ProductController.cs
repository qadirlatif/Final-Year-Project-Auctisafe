﻿using Auctisafe.Models;
using Auctisafe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Auctisafe.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        AuctionContext db = new AuctionContext();
        public ProductDetailsViewModel ProductDetailsViewModel(int id)
        {
            var MyProduct = db.Products.FirstOrDefault(x => x.Product_ID == id);
            var auction = db.auctions.FirstOrDefault(x => x.Product_ID == id);
            var status = db.auction_status.FirstOrDefault(x => x.Product_ID == id);
            var userdetails = db.signup.FirstOrDefault(x => x.User_ID == MyProduct.User_ID);
            ProductDetailsViewModel model = new ProductDetailsViewModel();
            var allbiddings = db.all_biddings.Where(x => x.Product_ID == MyProduct.Product_ID).OrderByDescending(x => x.Amount).ToList();
            List<BidAndBidder> bids = new List<BidAndBidder>();
            foreach (var bid in allbiddings)
            {
                var bidderdetails = db.signup.FirstOrDefault(x => x.User_ID == bid.Bidder_ID);
                bids.Add(new BidAndBidder { bidder = bidderdetails, bidding = bid });
            }
            model.userdetails = userdetails;
            model.biddingdetails = bids;
            model.product = MyProduct;
            model.auction = auction;
            model.status = status;
            return model;
        }
        public ActionResult Details(int id)
        {
            if (Convert.ToInt32(Session["id"]) != 0)
            {

                var model = ProductDetailsViewModel(id);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "MyAccount");
            }
        }
        public BidAndBidder maxfinder(List<BidAndBidder> allbiddings)
        {

            var maxbidder = new BidAndBidder
            {
                bidding = new biddings // Initialize the 'bidding' property
                {
                    Amount = 0 // Set the initial amount
                }
            };

            if (allbiddings != null)
            {
                foreach (var items in allbiddings)
                {
                    if (items.bidding != null && items.bidding.Amount > maxbidder.bidding.Amount)
                    {
                        maxbidder.bidding = items.bidding;
                        maxbidder.bidder = items.bidder;
                    }
                }
                return maxbidder;
            }
            else
            {
                return maxbidder;
            }
        }
        [HttpGet]
        public ActionResult bid(string amount, string productid)
        {
            var model = ProductDetailsViewModel(int.Parse(productid));
            if (model.auction.Auction_type_ID == 1)
            {
                var maxbidder = maxfinder(model.biddingdetails);
                if (float.Parse(amount) > maxbidder.bidding.Amount && float.Parse(amount) > model.product.price)
                {
                    if (model.auction.End_date > DateTime.Now)
                    {
                        biddings bid = new biddings();
                        Random rand = new Random();
                        bid.Bid_ID = rand.Next(99999, 999999);
                        bid.Product_ID = int.Parse(productid);
                        bid.Bidder_ID = Convert.ToInt32(Session["id"]);
                        bid.Amount = double.Parse(amount);
                        bid.Date = DateTime.Now;
                        
                        db.all_biddings.Add(bid);
                        db.SaveChanges();
                        return Content("bid success");
                    }
                    else
                    {
                        return Content("Auction Timeout!!");
                    }
                }
                else
                {
                    return Content("bid failed");
                }
            }
            else if (model.auction.Auction_type_ID == 3)
            {
                if (float.Parse(amount) > model.product.price)
                {
                    if (model.auction.End_date > DateTime.Now)
                    {
                        biddings bid = new biddings();
                        Random rand = new Random();
                        bid.Bid_ID = rand.Next(99999, 999999);
                        bid.Product_ID = int.Parse(productid);
                        bid.Bidder_ID = Convert.ToInt32(Session["id"]);
                        bid.Amount = double.Parse(amount);
                        bid.Date = DateTime.Now;
                        
                        db.all_biddings.Add(bid);
                        db.SaveChanges();
                        return Content("Congrats! Bid Success ");
                    }
                    else
                    {
                        return Content("Auction Timeout!!");
                    }
                }
                else
                {
                    return Content("Kindly Bid Above The Price has been set by Auctioneer");
                }
            }
            else if (model.auction.Auction_type_ID == 2)
            {
                if (model.status.Status == "A")
                {
                    if (model.auction.End_date > DateTime.Now)
                    {
                        if (float.Parse(amount) == model.auction.UpdatedPrice)
                        {
                            bid_winner biddingwinner = new bid_winner();
                            biddings bid = new biddings();
                            Random rand = new Random();
                            model.status.Status = "D";
                            db.Entry(model.status).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            bid.Bid_ID = rand.Next(99999, 999999);
                            bid.Product_ID = int.Parse(productid);
                            bid.Bidder_ID = Convert.ToInt32(Session["id"]);
                            bid.Amount = double.Parse(amount);
                            bid.Date = DateTime.Now;
                            
                            db.all_biddings.Add(bid);
                            db.SaveChanges();
                            biddingwinner.Bid_ID = bid.Bid_ID;
                            biddingwinner.Date = DateTime.Now;
                            db.bid_winner.Add(biddingwinner);
                            db.SaveChanges();
                            mailer mail = new mailer();
                            int userid = Convert.ToInt32(Session["id"]);
                            var user = db.login.Where(x => x.User_ID == userid).FirstOrDefault();
                            mail.Emailer(user.Email, "Congratulations!", "You won the bid on item : "+model.product.name);
                            return Content("Congrats! You Won the bid Kindly check your email!");

                        }
                        else if (float.Parse(amount) < model.auction.UpdatedPrice)
                        {
                            biddings bid = new biddings();
                            Random rand = new Random();
                            bid.Bid_ID = rand.Next(99999, 999999);
                            bid.Product_ID = int.Parse(productid);
                            bid.Bidder_ID = Convert.ToInt32(Session["id"]);
                            bid.Amount = double.Parse(amount);
                            bid.Date = DateTime.Now;
                            
                            
                            db.all_biddings.Add(bid);

                            db.SaveChanges();
                            return Content("Congrats! Bid Success ");

                        }
                        else
                        {
                            return Content("Kindly Bid Under The Price has been set by Auctioneer");
                        }
                    }
                    else
                    {
                        return Content("Auction Timeout!!");
                    }
                }
                else
                {
                    return Content("Auction Ended Sorry better luck next time!!");
                }
            }
            else
            {
                return Content("auction type is different");
            }

        }
    }
}