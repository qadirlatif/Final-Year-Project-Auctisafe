using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("Payment")]
    public class payment
    {
        public int ID { get; set; }

        public int ProductID { get; set; }
        public float Bid_Amount { get; set; }
        public DateTime date { get; set; }
        public DateTime DueDate { get; set; }
        public int WinnerID { get; set; }
        public int AuctioneerID { get; set; }
    }
}