using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("Categories")]
    public class product_categories
    {
        [Key]
        public int Category_ID { get; set; }
        public string Category_name { get; set; }


    }
}