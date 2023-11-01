using Auctisafe.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Auctisafe.Controllers
{
    public class MyaccountController : Controller
    {
        MD5_Algo md5hashing = new MD5_Algo();
        AuctionContext db = new AuctionContext();
        // GET: Myaccount
        public ActionResult Index()
        {
            return View(new signupmaster());
        }
        [HttpPost]
        public ActionResult Index(signupmaster myaccount)
        {
            
            string returnValue = md5hashing.hashedvalue(myaccount.Password);
            Random rand = new Random();
            Session["id"]=rand.Next(1,1000000);
            Session["first name"] = myaccount.First_name;
            Session["last name"]=myaccount.Last_name;
            Session["CNIC"] = myaccount.CNIC;
            Session["address"]=myaccount.Address;
            Session["phone"] = myaccount.Phone_number;
            Session["email"] = myaccount.Email;
            Session["password"]=  returnValue.ToString();
            Session["status"] = "A";
            return RedirectToAction("verification");
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
                        Session["id"] = accounts.User_ID;
                        return RedirectToAction("Index","dashboard");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {

                    return RedirectToAction("Index");
                }
            }
            catch(Exception x)
            {
                return RedirectToAction("Index");
            }
            
        }
        public ActionResult verification()
        {
            if (Convert.ToInt32(Session["id"]) != 0)
            {
                mailer sendmail = new mailer();
                sendmail.Email_sender(Convert.ToString(Session["email"]), Convert.ToInt32(Session["id"]));
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
            
            if (verify.verificationcode == Convert.ToInt32(Session["id"]))
            {
                Models.Signup accdetails = new Models.Signup()
                {
                    User_ID = Convert.ToInt32(Session["id"]),
                    First_name = Session["first name"].ToString(),
                    Last_name = Session["last name"].ToString(),
                    CNIC = Session["CNIC"].ToString(),
                    Address = Session["address"].ToString(),
                    Phone_number = Convert.ToInt32( Session["phone"]),
                };
                Models.Login acclogindetails = new Models.Login()
                {
                    User_ID = Convert.ToInt32(Session["id"]),
                    Email = Session["email"].ToString(),
                    Password = Session["password"].ToString(),
                    Status = Session["status"].ToString(),
                };
                Session.Clear();
                db.signup.Add(accdetails);
                db.SaveChanges();
                db.login.Add(acclogindetails);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
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
                mail.Emailer(user.Email, "Your Request for Forgot Password", "this is your verification code : "+code);
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

    }
}