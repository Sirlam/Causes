using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Causes.UI.Web.Models
{
    public class TB_SIGNATURES
    {
        [Key]
        public int ID { get; set; }
        public int CAUSE_ID { get; set; }
        public int USER_ID { get; set; }
        public DateTime? SIGNED_DATE { get; set; }
    }
}