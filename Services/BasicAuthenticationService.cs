using System;
using System.Text;
using CSM.Security.Models;
using Orchard.Mvc;
using Orchard.Security;

namespace CSM.Security.Services
{
    public class BasicAuthenticationService : IBasicAuthenticationService
    {
        private readonly IHttpContextAccessor _hca;
        private readonly IMembershipService _membershipService;
        private readonly IAuthenticationService _authenticationService;

        public BasicAuthenticationService(
            IHttpContextAccessor hca,
            IMembershipService membershipService,
            IAuthenticationService authenticationService)
        {
            _hca = hca;
            _membershipService = membershipService;
            _authenticationService = authenticationService;
        }

        public IBasicAuthenticationCredentials GetRequestCredentials()
        {
            var header = _hca.Current().Request.Headers["Authorization"];

            if (String.IsNullOrEmpty(header) ||
                !header.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var credentials = UTF8Encoding.UTF8.GetString(Convert.FromBase64String(header.Substring(6)));
            int separatorIndex = credentials.IndexOf(':');

            if (separatorIndex < 0)
                return null;

            return new BasicAuthenticationCredentials {
                Username = credentials.Substring(0, separatorIndex),
                Password = credentials.Substring(separatorIndex + 1)
            };
        }

        public IUser GetUserForRequest()
        {
            var credentials = GetRequestCredentials();

            if (credentials == null)
                return null;

            return _membershipService.ValidateUser(credentials.Username, credentials.Password);
        }

        public bool SetAuthenticatedUserForRequest()
        {
            var user = GetUserForRequest();

            if (user == null)
                return false;
            
            _authenticationService.SetAuthenticatedUserForRequest(user);
            
            return true;
        }
    }
}