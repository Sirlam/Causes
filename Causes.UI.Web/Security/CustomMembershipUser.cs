using Causes.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Causes.UI.Web.Security
{
    public class CustomMembershipUser : MembershipUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UserRoleId { get; set; }

        public string UserRoleName { get; set; }

        public string DisplayName { get; set; }

        public int ProfileId { get; set; }

        public CustomMembershipUser(TB_USERS user)
            :base("CustomMembershipProvider", user.USER_ID, user.ID, user.EMAIL, string.Empty, string.Empty, true, false,
                 (DateTime)user.CREATED_DATE, (DateTime)user.LAST_LOGIN_DATE, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            FirstName = user.FIRST_NAME;
            LastName = user.LAST_NAME;
            UserRoleId = (int)user.ROLE_ID;
            UserRoleName = user.TB_ROLES.ROLE_NAME;
            DisplayName = user.DISPLAY_NAME;
            ProfileId = (int)user.ID;
        }
    }
}