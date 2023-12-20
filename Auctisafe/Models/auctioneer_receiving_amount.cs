using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("Auctioneer_recieving_Amount")]
    public class auctioneer_receiving_amount
    {
        public int ID { get; set; }

        public int productID { get; set; }
        public float TotalAmount { get; set; }
        public int WinnerID { get; set; }
        public int AuctioneerID { get; set; }
    }
}