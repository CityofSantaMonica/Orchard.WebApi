using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using CSM.Security.Models;
using CSM.Security.Services;
using Orchard;
using Orchard.Logging;

namespace CSM.Security.Filters.Http
{
    /// <summary>
    /// Filter to enforce HTTP Basic authentication for the decorated WebApi controller/action.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireBasicAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public ILogger Logger { get; set; }

        public RequireBasicAuthenticationAttribute()
        {
            Logger = NullLogger.Instance;
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            HttpRequestMessage request = context.Request;

            var workContext = context.ActionContext.ControllerContext.GetWorkContext();

            var authenticator = workContext.Resolve<IBasicAuthenticationService>();

            var credentials = await Task.Run(() => authenticator.GetCredentials(request.Headers.Authorization));
            
            if (credentials == null)
            {
                Logger.Warning(
                    "Basic authentication failed: missing credentials {0} for {1}",
                    request.Method,
                    request.RequestUri
                );
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
                return;
            }
            
            var user = await Task.Run(() => authenticator.GetUserForCredentials(credentials));

            if (user == null)
            {
                Logger.Warning(
                    "Basic authentication failed: invalid credentials {0} {1} for {2}",
                    credentials.Username,
                    request.Method,
                    request.RequestUri
                );
                context.ErrorResult = new AuthenticationFailureResult("Invalid username or password", request);
            }
            else
            {
                authenticator.SetAuthenticatedUserForRequest(user);
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var challenge = new AuthenticationHeaderValue("Basic");
            context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);
            return Task.FromResult(0);
        }

        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}