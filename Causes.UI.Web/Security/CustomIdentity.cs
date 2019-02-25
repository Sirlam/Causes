using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace Causes.UI.Web.Security
{
    [Serializable]
    public class CustomIdentity : IIdentity
    {
        public IIdentity Identity { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int UserRoleId { get; set; }

        public string UserRoleName { get; set; }

        public string DisplayName { get; set; }

        public int ProfileId { get; set; }

        public string Name
        {
            get { return Identity.Name; }
        }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated { get { return Identity.IsAuthenticated;  } }

        public CustomIdentity(IIdentity identity)
        {
            Identity = identity;

            var customMembershipUser = (CustomMembershipUser)Membership.GetUser(HttpContext.Current.User.Identity.Name);
            if(customMembershipUser != null)
            {
                FirstName = customMembershipUser.FirstName;
                LastName = customMembershipUser.LastName;
                Email = customMembershipUser.Email;
                UserRoleId = customMembershipUser.UserRoleId;
                UserRoleName = customMembershipUser.UserRoleName;
                DisplayName = customMembershipUser.DisplayName;
                ProfileId = customMembershipUser.ProfileId;
            }
        }
    }
}