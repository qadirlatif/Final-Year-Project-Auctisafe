using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("Feedback")]
    public class feedback
    {
        public int ID { get; set; }

        public int Bidder_ID { get; set; }
        public int Auction_ID { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }


    }
}