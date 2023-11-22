using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auctisafe.Models
{
    [Table("Report")]
    public class report
    {
        
        public int Reporter_ID { get; set; }
        public int User_ID { get; set; }
        public string Reason { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        [Key]
        public string ReportID { get; set; }
    }
}