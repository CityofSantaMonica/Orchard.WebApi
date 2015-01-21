using System;
using System.Web.Mvc;
using CSM.Security.Services;
using Orchard;

namespace CSM.Security.Filters
{
    /// <summary>
    /// Filter to enforce HTTP Basic authentication for the decorated controller/action.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireBasicAuthenticationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var workContext = filterContext.Controller.ControllerContext.GetWorkContext();

            var authenticator = workContext.Resolve<IBasicAuthenticationService>();

            var user = authenticator.GetUserForRequest();

            if (user == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}