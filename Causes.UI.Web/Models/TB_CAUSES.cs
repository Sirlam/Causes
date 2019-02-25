using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Causes.UI.Web.Models
{
    [Table("TB_CAUSES")]
    public partial class TB_CAUSES
    {
        [Key]
        public int ID { get; set; }
        public string TOPIC { get; set; }
        public string DESCRIPTION { get; set; }
        public string IMG_URL { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
    }
}