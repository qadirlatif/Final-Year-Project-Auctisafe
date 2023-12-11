using Auctisafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auctisafe.ViewModel
{
    public class ReportViewModel
    {
    }
    public class UserReportViewModel
    {
        public report userreport { get; set; }
        public Signup reporterdetail { get; set; }
        public Signup againstdetail { get; set; }
    }
}