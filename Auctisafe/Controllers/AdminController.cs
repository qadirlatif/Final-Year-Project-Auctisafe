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
    }
}