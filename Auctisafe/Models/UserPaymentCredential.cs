using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    public class UserPaymentCredential
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string IBAN { get; set; }
        public string Accountname { get; set; }
        public string  Accountnumber { get; set; }
        public string Bank { get; set; }
    }
}