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
        [Key]
        public int Bid_ID { get; set; }
        public int Tax_rate { get; set; }
        public DateTime Date { get; set; }

    }
}