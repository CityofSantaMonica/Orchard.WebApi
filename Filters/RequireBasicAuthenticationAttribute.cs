using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using CSM.Security.Services;
using Orchard;
using Orchard.Security;
using Orchard.Security.Permissions;

namespace CSM.Security.Filters
{
    /// <summary>
    /// Authorize attribute to enforce HTTP Basic authentication with the provided Orchard permission(s) for access to the decorated controller/action(s).
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireBasicAuthorizationAttribute : AuthorizeAttribute
    {
        private readonly string[] _permissionNames;

        public RequireBasicAuthorizationAttribute() 
            : this(null)
        {
        }

        public RequireBasicAuthorizationAttribute(params string[] permissionNames)
        {
            _permissionNames = permissionNames;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var workContext = actionContext.ControllerContext.GetWorkContext();

            var user = workContext.Resolve<IBasicAuthenticationService>().GetUserForRequest();

            if (user == null)
                return false;

            var authorizationService = workContext.Resolve<IAuthorizationService>();

            if (_permissionNames == null)
                return true;

            var permissions = _permissionNames.Select(name => new Permission { Name = name });

            foreach (var permission in permissions)
            {
               if (!authorizationService.TryCheckAccess(permission, user, null))
                   return false;
            }

            return true;
        }
    }
}