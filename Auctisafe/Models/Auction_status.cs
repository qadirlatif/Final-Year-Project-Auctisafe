using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("Auction_Status")]
    public class Auction_status
    {
        
        public int Product_ID { get; set; }
        public string Status { get; set; }
        [Key]
        public string keyparam { get; set; }

    }
}