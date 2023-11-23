using Auctisafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Auctisafe.OtherLogicModels
{
    public class AutomatedReportsAction
    {
        private static Timer _timer;
        private static mailer emailService;
        public static void Start()
        {
            // Set the interval to 15 minutes (in milliseconds)
            int interval = 1 * 60 * 1000;
            emailService = new mailer();
            // Create a timer that calls the TimerCallback method once every 15 minutes
            _timer = new Timer(TimerCallback, null, 0, interval);
        }

        private static void TimerCallback(object state)
        {
            AuctionContext db=new AuctionContext();
            var users = db.login.Where(x=>x.Status == "A").ToList();
            foreach(var user in users)
            {
                int counts = db.Reports.Where(x => x.User_ID == user.User_ID).Distinct().Count();
                if(counts >= 3)
                {
                    user.Status = "D";
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    emailService.Emailer(user.Email, "Account Suspending", "Auctisafe Recieved many reports against you so temporary your account as been suspended");
                    db.SaveChanges();
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