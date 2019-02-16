using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Causes.UI.Web.ViewModels
{
    public class User
    {
        public User()
        {
            Roles = new List<SelectListItem>();
        }

        [Required(ErrorMessage ="Email is required", AllowEmptyStrings =false)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        public DateTime? CreatedDate { get; set; }

        [DisplayName("User Type")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "First name is required", AllowEmptyStrings = false)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required", AllowEmptyStrings = false)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public IList<SelectListItem> Roles { get; set; }
    }
}