using System;
using System.Linq;
using System.Web.Mvc;
using Orchard;
using Orchard.Security;
using Orchard.Security.Permissions;

namespace CSM.Security.Filters
{
    /// <summary>
    /// Authorize attribute to enforce authentication with the provided Orchard permission(s) for access to the decorated controller/action(s).
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireAuthorizationAttribute : AuthorizeAttribute
    {
        private readonly string[] _permissionNames;

        public RequireAuthorizationAttribute() 
            : this(null)
        {
        }

        public RequireAuthorizationAttribute(params string[] permissionNames)
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

            if (_permissionNames == null)
                return;

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