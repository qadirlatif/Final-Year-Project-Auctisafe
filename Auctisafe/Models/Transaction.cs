using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("transaction")]
    public class Transaction
    {
        public int ID { get; set; }
        public string TransactionID { get; set; }
        public int ProductID { get; set; }
    }
}