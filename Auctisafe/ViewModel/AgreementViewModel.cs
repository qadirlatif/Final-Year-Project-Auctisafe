using Auctisafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auctisafe.ViewModel
{
    public class AgreementViewModel
    {
        public Product  productdetails { get; set; }
        public Agreement winnerAgreement { get; set; }
        public Agreement AuctioneerAgreement { get; set; }

    }
}