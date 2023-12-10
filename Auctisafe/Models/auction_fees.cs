using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("Auction_fees")]
    public class auction_fees
    {
        public int ID { get; set; }

        public int Bid_ID { get; set; }
        public int Auction_fee_rate { get; set; }
        public DateTime date { get; set; }
    }
}