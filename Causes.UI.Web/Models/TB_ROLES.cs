using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Causes.UI.Web.Models
{
    public class TB_ROLES
    {
        [Key]
        public int ID { get; set; }
        public string ROLE_NAME { get; set; }
    }
}