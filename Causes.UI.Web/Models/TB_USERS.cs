using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Causes.UI.Web.Models
{
    public class TB_USERS
    {
        [Key]
        public int ID { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD_HASH { get; set; }
        public string PASSWORD_SALT { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public DateTime? LAST_LOGIN_DATE { get; set; }
        public int ROLE_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
    }
}