using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Causes.UI.Web.Security
{
    [Serializable]
    public class CustomPrincipal : IPrincipal
    {
        public bool IsInRole(string role)
        {
            return Identity is CustomIdentity &&
                string.Compare(role, ((CustomIdentity)Identity).UserRoleName, StringComparison.CurrentCultureIgnoreCase) == 0;
        }

        public IIdentity Identity { get; private set; }

        public CustomIdentity CustomIdentity { get { return (CustomIdentity)Identity; } set { Identity = value; } }

        public CustomPrincipal(CustomIdentity identity)
        {
            Identity = identity;
        }
    }
}