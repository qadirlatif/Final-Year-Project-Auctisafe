using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("Contact")]
    public class Contact
    {
        public int ID { get; set; }

        public int ContactID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public string Subject { get; set; }

        public string Body { get; set; }
       
    }
}