using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    public class Agreement
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string AgreementName { get; set; }
    }
}