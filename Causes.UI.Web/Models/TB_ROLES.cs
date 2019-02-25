using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Causes.UI.Web.Models
{
    [Table("TB_ROLES")]
    public partial class TB_ROLES
    {
        public TB_ROLES()
        {
            TB_USERS = new HashSet<TB_USERS>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string ROLE_NAME { get; set; }

        public virtual ICollection<TB_USERS> TB_USERS { get; set; }
    }
}