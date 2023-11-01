using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("auctions")]
    public class Auctions
    {
        
        public int Product_ID { get; set; }
        public int Auction_type_ID { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public int increament { get; set; }
        public int decreament { get; set; }
        public int auto_accept_amount { get; set; }
        public int IntervalinHours { get; set; }
        public int UpdatedPrice { get; set; }
        public DateTime currentdate { get; set; }
        [Key]

        public string keyparams { get; set; }

    }
}