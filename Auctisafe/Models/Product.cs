using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("Product")]
    public class Product
    {

        public int ID { get; set; }

        public int Product_ID { get; set; }
        
        public int User_ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int Category_ID { get; set; }
        public int price { get; set; }
        public string used { get; set; }
        public string image { get; set; }


    }
}