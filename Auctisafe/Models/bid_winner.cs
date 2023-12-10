using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("Bid_winner")]
    public class bid_winner
    {
        public int ID { get; set; }

        public int Bid_ID { get; set; }

        public DateTime Date { get; set; }
    }
}