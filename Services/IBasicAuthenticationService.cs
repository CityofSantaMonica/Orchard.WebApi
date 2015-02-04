using System.Net.Http;
using System.Net.Http.Headers;
using CSM.WebApi.Models;
using Orchard;
using Orchard.Environment.Extensions;
using Orchard.Security;

namespace CSM.WebApi.Services
{
    /// <summary>
    /// A service definition for performing HTTP Basic authentication.
    /// </summary>
    public interface IBasicAuthenticationService : IDependency
    {
        /// <summary>
        /// Gets the basic authentication credentials from the authentication header.
        /// </summary>
        /// <returns>The credentials or null if there are no credentials.</returns>
        BasicAuthenticationCredentials GetCredentials(AuthenticationHeaderValue header);

        /// <summary>
        /// Gets the Orchard user corresponding to the basic authentication credentials.
        /// </summary>
        /// <returns>The matching user if found or null if not.</returns>
        IUser GetUserForCredentials(BasicAuthenticationCredentials credentials);

        /// <summary>
        /// Sets the specified user as the authenticated user for the current request.
        /// </summary>
        /// <returns>True if the authentication was successful, false if not.</returns>
        bool SetAuthenticatedUserForRequest(IUser user);

        /// <summary>
        /// Sets the authenticated user from credentials found in the specified request.
        /// </summary>
        /// <param name="request">The HttpRequestMessage to be authenticated.</param>
        /// <param name="workContext">The Orchard WorkContext tied to this <paramref name="request"/>.</param>
        /// <returns>True if the authentication was successful, false if not.</returns>
        bool SetAuthenticatedUserForRequest(HttpRequestMessage request, WorkContext workContext);
    }
}
