using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    public class signupmaster
    {
        [Key]
        public int User_ID { get; set; }
        [DisplayName("first name")]
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public int Phone_number { get; set; }
        public string CNIC { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
    }
}