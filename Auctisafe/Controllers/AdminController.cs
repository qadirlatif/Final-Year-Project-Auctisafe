using Auctisafe.Models;
using Auctisafe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auctisafe.Controllers
{
    public class AdminController : Controller
    {
        AuctionContext db = new AuctionContext();
        // GET: Admin
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["adminID"]) != 0)
            {
                return View("Index", "_AdminLayout");
            }
            else
            {
                return RedirectToAction("Index", "Myaccount");
            }

        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Myaccount");
        }
        public ActionResult Users()
        {
            List<userviewmodel> model = new List<userviewmodel>();

            var allusers = db.signup.ToList();

            foreach (var users in allusers)
            {
                UserdetaiandproductslViewModel productsviewmodel = new UserdetaiandproductslViewModel();

                productsviewmodel.userdetails = db.signup.Where(x => x.User_ID == users.User_ID).FirstOrDefault();
                productsviewmodel.usercridentail = db.login.Where(x => x.User_ID == users.User_ID).FirstOrDefault();
                if (productsviewmodel.usercridentail.Role == "A")
                {
                    continue;
                }
                else
                {
                    model.Add(new userviewmodel { userdetailandproduct = productsviewmodel });
                }
            }
            return View("Users", "_AdminLayout", model);
        }
        public ActionResult Auctions()
        {
            List<AuctionListViewModel> model = new List<AuctionListViewModel>();
            var products = db.Products.ToList();
            foreach (var product in products)
            {
                AuctionListViewModel productdetail = new AuctionListViewModel();
                productdetail.AuctionID = product.Product_ID;
                var auction = db.auctions.Where(x => x.Product_ID == product.Product_ID).FirstOrDefault();
                productdetail.Auction = (db.auction_Types.Where(x => x.Auction_type_ID == auction.Auction_type_ID).FirstOrDefault()).Auction_name;
                var targetuser = db.signup.Where(x => x.User_ID == product.User_ID).FirstOrDefault();
                productdetail.Auctioneer_Name = targetuser.First_name + " " + targetuser.Last_name;
                productdetail.ProductName = product.name;
                //productdetail.statusstatus = db.auction_status.Where(x => x.Product_ID == product.Product_ID).FirstOrDefault();
                productdetail.userid = product.User_ID;
                productdetail.status = (db.auction_status.Where(x => x.Product_ID == product.Product_ID).FirstOrDefault()).Status;
                model.Add(productdetail);
            }
            return View("Auctions", "_AdminLayout", model);
        }
        [HttpGet]
        public ActionResult Userdetail(int id)
        {
            UserdetaiandproductslViewModel model = new UserdetaiandproductslViewModel();
            var logintarget = db.login.Where(x => x.User_ID == id).FirstOrDefault();
            var target = db.signup.Where(x => x.User_ID == id).FirstOrDefault();
            var products = db.Products.Where(x => x.User_ID == id).ToList();
            List<AuctionListViewModel> allauctions = new List<AuctionListViewModel>();
            foreach (var product in products)
            {
                AuctionListViewModel targetauction = new AuctionListViewModel();
                var auction = db.auctions.Where(x => x.Product_ID == product.Product_ID).FirstOrDefault();
                targetauction.ProductName = product.name;
                targetauction.AuctionID = product.Product_ID;
                targetauction.Auctioneer_Name = target.First_name + " " + target.Last_name;
                targetauction.status = (db.auction_status.Where(x => x.Product_ID == product.Product_ID).FirstOrDefault()).Status;
                targetauction.Auction = (db.auction_Types.Where(x => x.Auction_type_ID == auction.Auction_type_ID).FirstOrDefault()).Auction_name;
                allauctions.Add(targetauction);
            }
            var biddingsinAuction = db.all_biddings.Where(x => x.Bidder_ID == id).ToList();
            model.Biddings = biddingsinAuction;
            model.auctions = allauctions;
            model.userdetails = target;
            model.usercridentail = logintarget;
            return View("Userdetail", "_AdminLayout", model);
        }
        [HttpGet]
        public ActionResult UpdateUserDetail(signupmaster model)
        {
            var targetuser = db.login.Where(x => x.User_ID == model.User_ID).FirstOrDefault();
            targetuser.Email = model.Email;
            if (targetuser.Status == "A" && model.Status == "D")
            {
                mailer mail = new mailer();
                mail.Emailer(targetuser.Email, "Account Suspending", "Your Account has been Suspended By Auctisafe");
            }
            else if (targetuser.Status == "D" && model.Status == "A")
            {
                mailer mail = new mailer();
                mail.Emailer(targetuser.Email, "Account Activate", "Your Account has been Activated By Auctisafe");

            }
            else if(targetuser.Status == "P" && model.Status == "A")
            {
                mailer mail = new mailer();
                mail.Emailer(targetuser.Email,"Account Request", "Congrats! Your pending Account successfully Accepted Now You can Create Auction, bidding and other etc.");
            }
            targetuser.Status = model.Status;
            db.Entry(targetuser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            var targetuserdetail = db.signup.Where(x => x.User_ID == model.User_ID).FirstOrDefault();
            targetuserdetail.Phone_number = model.Phone_number;
            targetuserdetail.Address = model.Address;
            targetuserdetail.CNIC = model.CNIC;
            targetuserdetail.First_name = model.First_name;
            targetuserdetail.Last_name = model.Last_name;
            db.Entry(targetuserdetail).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Content("Account Updated");
        }
        [HttpGet]
        public ActionResult ProductDetails(int id)
        {
            ProductDetailsViewModel model = new ProductDetailsViewModel();
            model.product = db.Products.Where(x => x.Product_ID == id).FirstOrDefault();
            model.auction = db.auctions.Where(x => x.Product_ID == id).FirstOrDefault();
            model.status = db.auction_status.Where(x => x.Product_ID == id).FirstOrDefault();
            model.AuctioneerDetail = db.signup.Where(x => x.User_ID == model.product.User_ID).FirstOrDefault();
            var bidding = db.all_biddings.Where(x => x.Product_ID == id).ToList();
            List<BidAndBidder> biddings = new List<BidAndBidder>();
            foreach (var bid in bidding)
            {
                BidAndBidder biddingdetail = new BidAndBidder();
                var targetbidder = db.signup.Where(x => x.User_ID == bid.Bidder_ID).FirstOrDefault();
                biddingdetail.bidding = bid;
                biddingdetail.bidder = targetbidder;
                biddings.Add(biddingdetail);
            }
            model.biddingdetails = biddings;
            //ViewBag.category = (db.categories.Where(x => x.Category_ID == model.product.Category_ID).FirstOrDefault()).Category_name;
            return View("ProductDetails", "_AdminLayout", model);
        }
        [HttpGet]
        public ActionResult DeactivateProduct(int productid)
        {
            var targetauction = db.auction_status.Where(x => x.Product_ID == productid).FirstOrDefault();
            var product = db.Products.Where(x => x.Product_ID == productid).FirstOrDefault();
            targetauction.Status = "D";
            db.Entry(targetauction).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            EmailForAuction(product.User_ID, "Auction Deactivation", "Your Auction " + product.name + " has been deactivated by auctisafe");
            return Content("Product Deactivated!");
        }
        [HttpGet]
        public ActionResult ActivateProduct(int productid)
        {
            var targetauction = db.auction_status.Where(x => x.Product_ID == productid).FirstOrDefault();
            var product = db.Products.Where(x => x.Product_ID == productid).FirstOrDefault();
            targetauction.Status = "A";
            db.Entry(targetauction).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            EmailForAuction(product.User_ID, "Auction Activation", "Your Auction " + product.name + " has been Activated by auctisafe");
            return Content("Product Deactivated!");
        }
        public void EmailForAuction(int id, string subject, string body)
        {
            mailer mail = new mailer();
            var user = db.login.Where(x => x.User_ID == id).FirstOrDefault();
            mail.Emailer(user.Email, subject, body);

        }
        [HttpGet]
        public ActionResult RollBackBid(int id)
        {
            var targetbid = db.all_biddings.Where(x => x.Bid_ID == id).FirstOrDefault();
            db.all_biddings.Remove(targetbid);
            db.SaveChanges();
            var product = db.Products.Where(x => x.Product_ID == targetbid.Product_ID).FirstOrDefault();
            var userlogin = db.login.Where(x => x.User_ID == targetbid.Bidder_ID).FirstOrDefault();
            var usersinup = db.signup.Where(x => x.User_ID == targetbid.Bidder_ID).FirstOrDefault();
            mailer mail = new mailer();
            mail.Emailer(userlogin.Email, "Unnormal Bid Rollback", "Dear " + usersinup.First_name + " " + usersinup.Last_name + ", your bid on item " + product.name + " has been rollback because system detects some unnormal things whcih have done by your side. Thank You");

            return Content("Bid has been rollback");
        }
        [HttpGet]
        public ActionResult Announcement(string message = "", string subject = "")
        {
            if (message != "")
            {
                var users = db.login.Select(x => x.Email).ToList();
                string recipients = string.Join(",", users);

                mailer mail = new mailer();
                mail.Emailer(recipients, subject, message);
            }
            return View("Announcement", "_AdminLayout");
        }
        [HttpGet]
        public ActionResult PendingAccounts()
        {
            List<userviewmodel> model = new List<userviewmodel>();
            var targetuser = db.login.Where(x => x.Status == "P").ToList();
            foreach (var users in targetuser)
            {
                UserdetaiandproductslViewModel usermodel = new UserdetaiandproductslViewModel();
                usermodel.usercridentail = db.login.Where(x => x.User_ID == users.User_ID).FirstOrDefault();
                usermodel.userdetails = db.signup.Where(x => x.User_ID == users.User_ID).FirstOrDefault();
                model.Add(new userviewmodel { userdetailandproduct = usermodel });
            }
            return View("PendingAccounts", "_AdminLayout", model);
        }


        public ActionResult Reporting()
        {

            List<UserReportViewModel> model = new List<UserReportViewModel>();
            var allreport = db.Reports.ToList();
            foreach (var report in allreport)
            {
                UserReportViewModel reportdetail = new UserReportViewModel();
                reportdetail.userreport = db.Reports.Where(x => x.ReportID == report.ReportID).FirstOrDefault();
                reportdetail.reporterdetail = db.signup.Where(x => x.User_ID == report.Reporter_ID).FirstOrDefault(); //REPORTER
                //reportdetail.reportproduct = db.Products.Where(x => x.Product_ID == report.prod).FirstOrDefault(); //PRODUCT
                reportdetail.againstdetail = db.signup.Where(x => x.User_ID == report.User_ID).FirstOrDefault();
                model.Add(reportdetail);
            }

            return View("Reporting", "_AdminLayout", model);
        }

        [HttpGet]
        public ActionResult RejectReport(int id = 0)
        {
            var report = db.Reports.Where(x=>x.ReportID == id.ToString()).FirstOrDefault();
            db.Reports.Remove(report);
            db.SaveChanges();
            return Content("Report rejected & Deleted From DB");
        }
        [HttpGet]
        public ActionResult BlockUser(int id = 0)
        {
            var targetuser = db.login.Where(x => x.User_ID == id).FirstOrDefault();
            targetuser.Status = "D";
            db.SaveChanges();
            mailer mail = new mailer();
            mail.Emailer(targetuser.Email, "Account Suspending", "Your Account Has been Suspended by Auctisafe Kindly Contact to Auctisafe For Activate via Email");
            return Content("Reported User Blocked");
        }
    }
}