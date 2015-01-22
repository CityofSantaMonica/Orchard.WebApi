using System;
using System.Net.Http.Headers;
using System.Text;
using CSM.Security.Models;
using Orchard.Security;

namespace CSM.Security.Services
{
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
            if (credentials == null)
                return null;

            return _membershipService.ValidateUser(credentials.Username, credentials.Password);
        }

        public bool SetAuthenticatedUserForRequest(IUser user)
        {
            if (user == null)
                return false;

            _authenticationService.SetAuthenticatedUserForRequest(user);

            return true;
        }
    }
}