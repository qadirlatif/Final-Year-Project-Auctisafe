using Auctisafe.Models;
using Auctisafe.ViewModel;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.History;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Auctisafe.Controllers
{
    public class DashboardController : Controller
    {
        AuctionContext db = new AuctionContext();
        // GET: Dashboard
        public ActionResult Index()
        {

            if (Convert.ToInt32(Session["id"]) != 0)
            {
                int id = Convert.ToInt32(Session["id"]);
                List<Product> allproductlist = db.Products.Where(x => x.User_ID == id).ToList();
                Session["productCount"] = allproductlist.Count;
                return View(db.signup.First(x => x.User_ID == id));
            }
            else
            {
                return RedirectToAction("Index", "Myaccount");
            }
        }
        public ActionResult details()
        {
            if (Convert.ToInt32(Session["id"]) != 0)
            {
                int id = Convert.ToInt32(Session["id"]);
                return View(db.signup.First(x => x.User_ID == id));
            }
            else
            {
                return RedirectToAction("Index", "Myaccount");
            }
        }
        [HttpPost]
        public ActionResult details(Signup details)
        {
            ViewData["test"] = 5;
            try
            {

                int id = Convert.ToInt32(Session["id"]);
                Signup target = db.signup.First(x => x.User_ID == id);
                target.First_name = details.First_name;
                target.Last_name = details.Last_name;
                target.Address = details.Address;
                target.CNIC = details.Address;
                target.Phone_number = details.Phone_number;
                db.signup.AddOrUpdate(target);
                db.SaveChanges();


                return View();
            }
            catch (Exception x)
            {
                return RedirectToAction("Index", "Myaccount");
            }

        }
        public ActionResult Create_Auction()
        {
            if (Convert.ToInt32(Session["id"]) != 0)
            {
                List<SelectListItem> categories = new List<SelectListItem>() {
                new SelectListItem(){Text="Select Category please",Selected=true},
                new SelectListItem() { Text = "Electronics", Value = "1" },
               new SelectListItem() { Text = "cars", Value = "2" },
            new SelectListItem() { Text = "groceries", Value = "3" }
        };
                List<SelectListItem> auctions = new List<SelectListItem>() {
                new SelectListItem(){Text="Select Auction please",Selected=true},
                new SelectListItem() { Text = "English Auction", Value = "1" },
               new SelectListItem() { Text = "Dutch Auction", Value = "2" },
            new SelectListItem() { Text = "Sealed Bid Auction", Value = "3" },
            new SelectListItem() { Text = "Reverse Auction", Value = "4" },
            new SelectListItem() { Text = "Reserve Auction", Value = "5" },
            new SelectListItem() { Text = "Forward Auction", Value = "6" }
        };
                List<SelectListItem> used = new List<SelectListItem>()
            {
                new SelectListItem{Text="Select please", Selected=true},
                new SelectListItem(){Text="yes", Value="1"},
                new SelectListItem(){Text="no", Value="2"},
            };
                ViewBag.used = used;
                ViewBag.auctions = auctions;
                ViewBag.categories = categories;
                return View(new ProductAuctionMaster());
            }
            else
            {
                return RedirectToAction("Index", "MyAccount");
            }
        }
        [HttpPost]
        public ActionResult Create_Auction(HttpPostedFileBase file)
        {
            try
            {
                Random rand = new Random();
                int productID = rand.Next(9999, 10000000);
                //fridge.png
                //541255
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string updatedname = productID + ImageName;     //541255fridge.png
                string physicalPath = Server.MapPath("~/Images/" + updatedname);
                file.SaveAs(physicalPath);
                Product mypro = new Product()
                {
                    User_ID = Convert.ToInt32(Session["id"]),
                    Product_ID = productID,
                    image = updatedname,
                    used = (Request.Form["used"] == "1") ? "yes" : "no",
                    price = Convert.ToInt32(Request.Form["price"]),
                    description = Request.Form["description"],
                    name = Request.Form["name"].ToString(),
                    Category_ID = Convert.ToInt32(Request.Form["category"]),
                };
                db.Products.Add(mypro);
                db.SaveChanges();
                Auctions myauctions = new Auctions();

                myauctions.Auction_type_ID = Convert.ToInt32(Request.Form["auction"]);
                myauctions.Product_ID = productID;
                myauctions.Start_date = Convert.ToDateTime(Convert.ToDateTime(Request.Form["startdate"]).Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                myauctions.End_date = Convert.ToDateTime(Convert.ToDateTime(Request.Form["enddate"]).Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                myauctions.increament = 0;
                myauctions.auto_accept_amount = Convert.ToInt32(Request.Form["reservedprice"]);
                myauctions.decreament = Convert.ToInt32(Request.Form["decreament"]);
                string intervalvalue = Request.Form["Interval"];
                if (!string.IsNullOrEmpty(intervalvalue))
                {
                    myauctions.IntervalinHours = Convert.ToInt32(Request.Form["Interval"]);

                }
                else
                {
                    myauctions.IntervalinHours = 0;


                }
                myauctions.keyparams = productID.ToString();
                myauctions.UpdatedPrice = Convert.ToInt32(Request.Form["price"]);
                myauctions.currentdate = DateTime.Now;



                db.auctions.Add(myauctions);
                db.SaveChanges();
                Auction_status mystatus = new Auction_status()
                {
                    Product_ID = productID,
                    Status = "A",
                    keyparam = productID.ToString()
                };
                db.auction_status.Add(mystatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception x)
            {
                return RedirectToAction("Create_Auction");
            }


        }
        public ActionResult myauctions()
        {
            if (Convert.ToInt32(Session["id"]) != 0)
            {

                int id = Convert.ToInt32(Session["id"]);
                ViewBag.id = Convert.ToInt32(Session["id"]);
                List<Product> allproductlist = db.Products.Where(x => x.User_ID == id).ToList();

                return View(allproductlist);
            }
            else
            {
                return RedirectToAction("Index", "MyAccount");
            }
        }
        public ActionResult logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Myaccount");
        }
        [HttpGet]
        public ActionResult PendingPayments()
        {
            List<PendingPaymentViewModel> model = new List<PendingPaymentViewModel>();
            if (Convert.ToInt32(Session["id"]) != 0)
            {
                int id = Convert.ToInt32(Session["id"]);
                var pendingPayments = db.Payment.Where(x => x.WinnerID == id).ToList();
                foreach (var item in pendingPayments)
                {
                    var trx = db.transactions.Where(x => x.ProductID == item.ProductID).FirstOrDefault();
                    bool check = false;
                    if (trx != null)
                    {
                        check = true;
                    }
                    else
                    {
                        check = false;
                    }
                    model.Add(new PendingPaymentViewModel
                    {
                        isReceived = check ? "yes" : "no",
                        product = db.Products.Where(x => x.Product_ID == item.ProductID).FirstOrDefault(),
                        Amount = item.Bid_Amount
                    });

                }
                return View(model);
            }

            else
            {
                return RedirectToAction("Index", "Myaccount");
            }
        }
        [HttpGet]
        public ActionResult ReceivePayments()
        {
            List<ReceivePaymentVewmodel> model = new List<ReceivePaymentVewmodel>();
            if (Convert.ToInt32(Session["id"]) != 0)
            {
                int id = Convert.ToInt32(Session["id"]);
                var receivepayments = db.auctioneer_recieving_amount.Where(x => x.AuctioneerID == id).ToList();
                foreach (var item in receivepayments)
                {
                    model.Add(new ReceivePaymentVewmodel
                    {
                        totalamount = db.Payment.Where(x => x.ProductID == item.productID).FirstOrDefault().Bid_Amount,
                        Products = db.Products.Where(x => x.Product_ID == item.productID).FirstOrDefault(),
                        netamount = item.TotalAmount,
                        fees = db.auction_fees.Where(x => x.productID == item.productID).FirstOrDefault().Auctionfees,
                        tax = db.Tax.Where(x => x.productID == item.productID).FirstOrDefault().TaxAmount
                    });
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Myaccount");
            }

        }
        [HttpPost]
        public ActionResult SendTRX(HttpPostedFileBase image, string ProductID = "")
        {

            string filename = Path.GetFileName(image.FileName);
            string updatedname = filename + ProductID;
            string physicalPath = Server.MapPath("~/Images/" + updatedname);
            image.SaveAs(physicalPath);
            Transaction transaction = new Transaction();
            transaction.ProductID = int.Parse(ProductID);
            transaction.TransactionImage = updatedname;
            db.transactions.Add(transaction);
            db.SaveChanges();
            return Content("TRX ID Submitted");
        }
        [HttpGet]
        public ActionResult PaymentCredentials()
        {
            int id = Convert.ToInt32(Session["id"]);
            if (id != 0)
            {
                var targetcredential = db.UserPaymentcreditials.Where(x => x.UserID == id).FirstOrDefault();
                if (targetcredential != null)
                {
                    return View(targetcredential);
                }
                else
                {

                    return View(new UserPaymentCredential());
                }

            }
            else
            {
                return RedirectToAction("Index", "Myaccount");
            }
        }
        [HttpPost]
        public ActionResult PaymentCredentials(UserPaymentCredential model)
        {
            if (model.ID == 0)
            {
                model.UserID = Convert.ToInt32(Session["id"]);
                db.UserPaymentcreditials.Add(model);
                db.SaveChanges();
            }
            else
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Agreement()
        {
            var id = Convert.ToInt32(Session["id"]);
            if (id != 0)
            {
                List<AgreemtnViewModel> model = new List<AgreemtnViewModel>();
                var DueAgreement = db.Agreements.Where(x => x.UserID == id).ToList();
                foreach (var item in DueAgreement)
                {
                    var targetprod = db.Products.Where(x => x.Product_ID == item.ProductID).FirstOrDefault();
                    model.Add(new AgreemtnViewModel
                    {
                        ProductID = targetprod.Product_ID,
                        ProductName = targetprod.name,
                        AgreementName = item.AgreementName
                    });
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Myaccount");
            }
        }
        [HttpPost]
        public ActionResult SendAgreement()
        {
            var productid = Convert.ToInt32( Request.Form["productid"]);
            var userid = Convert.ToInt32(Session["id"]);
            var file = Request.Files["file"];
            string Filename = System.IO.Path.GetFileName(file.FileName);
            string updatedname = productid.ToString() + userid.ToString() + Filename;
            string physicalPath = Server.MapPath("~/Agreements/" + updatedname);
            file.SaveAs(physicalPath);
            var target = db.Agreements.Where(x => x.ProductID == productid && x.UserID == userid).FirstOrDefault();
            target.AgreementName = updatedname;
            db.Entry(target).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Content("Agreement Sent successfully");
        }
    }
}