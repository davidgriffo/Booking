using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Dll;

namespace Booking.Authorization {
    public class RequireSuperAdmin : AuthorizeAttribute {
        protected override bool AuthorizeCore(HttpContextBase httpContext) {
            //if user is admin we return true
            var user = new DllFacade().GetAccountGateway().GetUserLoggedIn();
            return user != null && user.IsSuperAdmin;
        }
        
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
        }
    }
}