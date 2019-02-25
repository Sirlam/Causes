using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Causes.UI.Web.Models
{
    [Table("TB_USERS")]
    public partial class TB_USERS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string USER_ID { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD_HASH { get; set; }
        public string PASSWORD_SALT { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public DateTime? LAST_LOGIN_DATE { get; set; }
        public int ROLE_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string DISPLAY_NAME { get; set; }

        public virtual TB_ROLES TB_ROLES { get; set; }
    }
}