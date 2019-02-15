using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Causes.UI.Web.ViewModels
{
    public class User
    {
        [Required(ErrorMessage ="Email is required", AllowEmptyStrings =false)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}