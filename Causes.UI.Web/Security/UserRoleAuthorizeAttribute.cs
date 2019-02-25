using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Causes.UI.Web.Security
{
    public class UserRoleAuthorizeAttribute : AuthorizeAttribute
    {
        public UserRoleAuthorizeAttribute() { }

        public UserRoleAuthorizeAttribute(params UserRole[] roles)
        {
            Roles = string.Join(",", roles.Select(r => r.ToString()));
        }
    }

    public enum UserRole
    {
        User = 1,
        Admin = 2
    }
}