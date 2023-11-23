using Auctisafe.Models;
using Auctisafe.OtherLogicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace Auctisafe
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutomatedReportsAction.Start();
            AutomationBidWinnerDetector.Start();
            AuctionTimerObserver.Start();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
