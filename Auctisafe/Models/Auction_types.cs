using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("Auction_Types")]
    public class Auction_types
    {
        [Key]
        public int Auction_type_ID { get; set; }
        public string Auction_name { get; set; }
    }
}