using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    public class verification
    {
        [Key]
        public int id { get; set; }
        public int verificationcode { get; set; }
    }
}