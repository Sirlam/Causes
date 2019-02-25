﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Causes.UI.Web.Security
{
    public class TimeAuthorizeAttribute : AuthorizeAttribute
    {
        public int StartTime { get; set; }
        public int EndTime { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (DateTime.Now.Hour < StartTime)
                return false;

            if (EndTime <= DateTime.Now.Hour)
                return false;

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
        }
    }
}