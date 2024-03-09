using Auctisafe.Models;
using Auctisafe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace Auctisafe.Controllers
{
    public class WelcomeController : Controller
    {
        // GET: Welcome
        AuctionContext db = new AuctionContext();
        public ActionResult Index()
        {
            var products = db.Products.ToList();
            ProductController pc = new ProductController();
            List<ProductDetailsViewModel> productDetails = new List<ProductDetailsViewModel>();
            foreach(var product in products)
            {
               productDetails.Add( pc.ProductDetailsViewModel(product.Product_ID));
                
            }
                return View(productDetails);
            




        }
        [HttpGet]
        public ActionResult BrowseProduct()
        {

            return View();
        }
        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {   
                return View();
            
        }
        [HttpGet]
        public ActionResult ContactSave(Contact contactinfo)
        {
            try
            {
                Random rand = new Random();
                int id = rand.Next(9999, 99999);
                contactinfo.ContactID = id;
                db.Contacts.Add(contactinfo);
                db.SaveChanges();
                mailer mail = new mailer();
                mail.Emailer("Auctisafe@gmail.com", contactinfo.Subject,
                "<h2>Email : "+contactinfo.Email+"</h2><br/>" +
                "<h2>Phone : "+contactinfo.phone+"</h2><br/>" +
                "<h2>Message : </h2><br/>" +
                "<p>"+contactinfo.Body+"</p>");
                return Content("Your Response Have been Recorded");
            }
            catch(Exception x)
            {
                return Content(x.Message);
            }
        }
    }
}