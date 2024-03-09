using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("USER")]
    
    public class Signup
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public int Phone_number { get; set; }
        public string CNIC { get; set; }
        public string Address { get; set; }
    }
    
}