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

        public int Bid_ID { get; set; }
        public float Final_Amount { get; set; }
    }
}