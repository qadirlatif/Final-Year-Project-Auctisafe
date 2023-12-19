using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("Payment")]
    public class payment
    {
        public int ID { get; set; }

        public int Bid_ID { get; set; }
        public float Bid_Amount { get; set; }
        public DateTime date { get; set; }
        public DateTime DueDate { get; set; }
    }
}