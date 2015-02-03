using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using CSM.WebApi.Models;
using Orchard;
using Orchard.Environment.Extensions;
using Orchard.Security;

namespace CSM.WebApi.Services
{
    [OrchardFeature("CSM.WebApi.Security")]
    public class BasicAuthenticationService : IBasicAuthenticationService
    {
        private readonly IMembershipService _membershipService;
        private readonly IAuthenticationService _authenticationService;

        public BasicAuthenticationService(
            IMembershipService membershipService,
            IAuthenticationService authenticationService)
        {
            _membershipService = membershipService;
            _authenticationService = authenticationService;
        }

        public BasicAuthenticationCredentials GetCredentials(AuthenticationHeaderValue header)
        {
            if (header == null || header.Scheme != "Basic" || String.IsNullOrEmpty(header.Parameter))
            {
                return null;
            }

            var credentials = UTF8Encoding.UTF8.GetString(Convert.FromBase64String(header.Parameter));
            int separatorIndex = credentials.IndexOf(':');

            if (separatorIndex < 0)
                return null;

            return new BasicAuthenticationCredentials {
                Username = credentials.Substring(0, separatorIndex),
                Password = credentials.Substring(separatorIndex + 1)
            };
        }

        public IUser GetUserForCredentials(BasicAuthenticationCredentials credentials)
        {
            return getUserForCredentials(credentials, _membershipService);
        }

        public bool SetAuthenticatedUserForRequest(IUser user)
        {
            return setAuthenticatedUserForRequest(user, _authenticationService);
        }
        
        public bool SetAuthenticatedUserForRequest(HttpRequestMessage request, WorkContext workContext)
        {
            var membershipService = workContext.Resolve<IMembershipService>();
            var authenticationService = workContext.Resolve<IAuthenticationService>();

            var credentials = GetCredentials(request.Headers.Authorization);
            var user = getUserForCredentials(credentials, membershipService);
            return setAuthenticatedUserForRequest(user, authenticationService);
        }

        private IUser getUserForCredentials(
            BasicAuthenticationCredentials credentials,
            IMembershipService membershipService)
        {
            if (credentials == null)
                return null;

            return membershipService.ValidateUser(credentials.Username, credentials.Password);
        }

        private bool setAuthenticatedUserForRequest(
            IUser user,
            IAuthenticationService authenticationService)
        {
            if (user == null)
                return false;

            authenticationService.SetAuthenticatedUserForRequest(user);

            return true;
        }
    }
}