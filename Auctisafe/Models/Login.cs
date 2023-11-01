using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("User_Account")]
    public class Login
    {
        public int User_ID { get; set; }

        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
    }
}