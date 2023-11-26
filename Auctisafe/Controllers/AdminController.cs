using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auctisafe.Controllers
{
    public class AdminController : Controller
    {
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
            return View("Users","_AdminLayout");
        }
        public ActionResult Auctions()
        {
            return View("Auctions","_AdminLayout");
        }
    }
}