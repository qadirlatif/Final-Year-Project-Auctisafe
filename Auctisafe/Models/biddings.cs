using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("Biddings")]
    public class biddings
    {
        public int Bid_ID { get; set; }

        public int Product_ID { get; set; }
        public int Bidder_ID { get; set; }
        
        public double Amount { get; set; }
        [Key]
        public DateTime Date { get; set; }
        
        

    }
}