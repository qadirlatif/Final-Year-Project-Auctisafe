using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("Tax_on_Auction")]
    public class Tax
    {
        public int ID { get; set; }

        public int productID { get; set; }
        public float TaxAmount { get; set; }
        public DateTime Date { get; set; }
        public int WinnerID { get; set; }
        public int AuctioneerID { get; set; }

    }
}