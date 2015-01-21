using System;
using System.Linq;
using System.Web.Mvc;
using Orchard;
using Orchard.Security;
using Orchard.Security.Permissions;

namespace CSM.Security.Filters
{
    /// <summary>
    /// Filter to require the specified Orchard permission(s) for the decorated controller/action.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequirePermissionsAttribute : AuthorizeAttribute
    {
        private readonly string[] _permissionNames;

        public RequirePermissionsAttribute(params string[] permissionNames)
        {
            _permissionNames = permissionNames;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var workContext = filterContext.Controller.ControllerContext.GetWorkContext();

            var user = workContext.CurrentUser;

            if (user == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            var authorizer = workContext.Resolve<IAuthorizer>();

            var permissions = _permissionNames.Select(name => new Permission { Name = name });

            foreach (var permission in permissions)
            {
                if (!authorizer.Authorize(permission))
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                    return;
                }
            }
        }
    }
}