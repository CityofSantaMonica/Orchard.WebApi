using CSM.Security.Models;
using Orchard;
using Orchard.Security;

namespace CSM.Security.Services
{
    public interface IBasicAuthenticationService : IDependency
    {
        /// <summary>
        /// Returns the basic authentication credentials available in the current request.
        /// </summary>
        /// <returns>The credentials or null if there are no credentials.</returns>
        BasicAuthenticationCredentials GetRequestCredentials();

        /// <summary>
        /// Tries to get the Orchard user corresponding to the basic authentication credentials.
        /// </summary>
        /// <returns>The matching user if found or null if not.</returns>
        IUser GetUserForRequest();

        /// <summary>
        /// Sets the authenticated user for the current request if the user could be authenticated
        /// with the basic auth credentials.
        /// </summary>
        /// <returns>True if the authentication was successful, false if not.</returns>
        bool SetAuthenticatedUserForRequest();
    }
}
