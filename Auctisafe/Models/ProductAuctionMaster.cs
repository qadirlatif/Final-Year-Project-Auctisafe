using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    public class ProductAuctionMaster
    {
        [Key]
        public int Product_ID { get; set; }
        public int User_ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int Category_ID { get; set; }
        public int price { get; set; }
        public string used { get; set; }
        public string image { get; set; }
        public int Auction_type_ID { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public int increament { get; set; }
        public int decreament { get; set; }
        public int auto_accept_amount { get; set; }
        public string Status { get; set; }
        public string username { get;set ; }
        public string Address { get; set; }
    }
}