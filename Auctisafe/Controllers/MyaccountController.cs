using Auctisafe.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using System.Web.UI.WebControls;

namespace Auctisafe.Controllers
{
    public class MyaccountController : Controller
    {
        MD5_Algo md5hashing = new MD5_Algo();
        AuctionContext db = new AuctionContext();
        // GET: Myaccount
        [HttpGet]
        public ActionResult Index()
        {

            return View(new signupmaster());
        }
        [HttpPost]
        public ActionResult Index(signupmaster myaccount)
        {
            //Validation for avoiding same email accounts
            var target = db.login.Where(x => x.Email == myaccount.Email).FirstOrDefault();
            if (target == null)
            {
                string returnValue = md5hashing.hashedvalue(myaccount.Password);
                Random rand = new Random();
                Session["userid"] = rand.Next(1, 1000000);
                Session["first name"] = myaccount.First_name;
                Session["last name"] = myaccount.Last_name;
                Session["CNIC"] = myaccount.CNIC;
                Session["address"] = myaccount.Address;
                Session["phone"] = myaccount.Phone_number;
                Session["email"] = myaccount.Email;
                Session["password"] = returnValue.ToString();
                Session["status"] = "P";
                return RedirectToAction("verification");
            }
            else
            {
                ViewBag.error = "Entered Email Already Registered!";
                return View();
            }

            
        }

        public ActionResult login(signupmaster myaccount)
        {
            
            try
            {
                var accounts = db.login.Where(x => x.Email == myaccount.Email).Single();
                if (accounts != null)
                {
                    string password=md5hashing.hashedvalue(myaccount.Password);

                    if (password.Equals(accounts.Password))
                    {
                        if (accounts.Status == "A")
                        {
                            if (accounts.Role == "A")
                            {
                                Session["adminID"] = accounts.User_ID;
                                return RedirectToAction("Index", "Admin");
                            }
                            else
                            {
                                Session["id"] = accounts.User_ID;
                                return RedirectToAction("Index", "dashboard");
                            }
                        }
                        else if(accounts.Status == "P")
                        {
                            ViewBag.error = "Your Account is currently In Pending, First Admin will approve then you will be able to create auction , bidding etc, when admin activate your account you will receive email on registered mail. Thank You";
                            return View("Index");
                        }
                        else 
                        {
                            ViewBag.error = "Your Account is currently suspended Please Contact Admin";
                            return View("Index");
                            //return RedirectToAction("AccountSuspended", "Myaccount");
                        }
                    }
                    else
                    {
                        ViewBag.error = "Please Enter Correct Credentials";
                        return View("Index");
                    }
                }
                else
                {
                    ViewBag.error = "Please Enter Correct Credentials";

                    return View("Index");
                }
            }
            catch(Exception x)
            {
                ViewBag.error = "Please Enter Correct Credentials";

                return View("Index");
            }
            
        }
        public ActionResult verification()
        {
            if (Convert.ToInt32(Session["userid"]) != 0)
            {
                mailer sendmail = new mailer();
                sendmail.Email_sender(Convert.ToString(Session["email"]), Convert.ToInt32(Session["userid"]));
                return View();
            }
            else
            {
                return RedirectToAction("index");
            }
            
        }
        [HttpPost]
        public ActionResult verification(verification verify)
        {
            
            if (verify.verificationcode == Convert.ToInt32(Session["userid"]))
            {
                Models.Signup accdetails = new Models.Signup()
                {
                    User_ID = Convert.ToInt32(Session["userid"]),
                    First_name = Session["first name"].ToString(),
                    Last_name = Session["last name"].ToString(),
                    CNIC = Session["CNIC"].ToString(),
                    Address = Session["address"].ToString(),
                    Phone_number = Convert.ToInt32( Session["phone"]),
                };
                Models.Login acclogindetails = new Models.Login()
                {
                    User_ID = Convert.ToInt32(Session["userid"]),
                    Email = Session["email"].ToString(),
                    Password = Session["password"].ToString(),
                    Status = Session["status"].ToString(),
                    Role = "N"
                };
                Session.Clear();
                db.signup.Add(accdetails);
                db.SaveChanges();
                db.login.Add(acclogindetails);
                db.SaveChanges();
                ViewBag.error = "Account Successfully Created, Now Your Account Status Is Pending, Currently Main Options Like Create Auction, Bidding etc are Blocked, When Your Account Activate You will Recieved Email On Registered Email.";
                return View("index");
            }
            else
            {
                ViewBag.error = "Please Enter Correct Code";
                return View();
            }
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            var user = db.login.Where(x => x.Email == Email).FirstOrDefault();
            if (user != null)
            {
                mailer mail = new mailer();
                Random rand = new Random();
                int code = rand.Next(1, 1000000);
                mail.Emailer(user.Email, "Your Request for Forgot Password", "this is your verification code : " + code);
                Session["code"] = code;
                Session["email"] = Email;
                return Content("true");
            }
            else
            {
                return Content("false");
            }

        }
        [HttpPost]
        public ActionResult verify(int Code)
        {
            var code = Convert.ToInt32(Session["code"]);
            if (code == Code)
            {
                return Content("true");
            }
            else
            {
                return Content("false");
            }
        }
        [HttpPost]
        public ActionResult changepass(string password)
        {
            MD5_Algo hash = new MD5_Algo();
            string hashedpass = hash.hashedvalue(password);
            string targetemail = Session["email"].ToString();
            if (targetemail != "")
            {
                var target = db.login.Where(x => x.Email == targetemail).FirstOrDefault();
                target.Password = hashedpass;
                db.Entry(target).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Content("your password has been changed successfully!");
            }
            else
            {
                return Content("You haven't entered email! Kindly fill ");
            }
        }
        [HttpGet]
        public ActionResult Report(string userid)
        {
            ViewBag.userid = userid;    
            return View();
        }
        [HttpPost]
        public ActionResult Report(report myreport)
        {
            Random rand = new Random();
            myreport.Reporter_ID = Convert.ToInt32(Session["id"]);
            myreport.date = DateTime.Now;
            myreport.ReportID = rand.Next(1, 1000000).ToString();
            db.Reports.Add(myreport);
            db.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult AccountSuspended()
        {
            return View();
        }



    }
}