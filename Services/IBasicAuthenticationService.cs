using System.Net.Http.Headers;
using CSM.Security.Models;
using Orchard;
using Orchard.Security;

namespace CSM.Security.Services
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
    }
}
