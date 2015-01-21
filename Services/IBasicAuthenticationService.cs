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
        /// Gets the basic authentication credentials in the current request.
        /// </summary>
        /// <returns>The credentials or null if there are no credentials.</returns>
        BasicAuthenticationCredentials GetRequestCredentials();

        /// <summary>
        /// Gets the Orchard user corresponding to the basic authentication credentials in the current request.
        /// </summary>
        /// <returns>The matching user if found or null if not.</returns>
        IUser GetUserForRequest();

        /// <summary>
        /// Sets the authenticated user for the current request.
        /// </summary>
        /// <returns>True if the authentication was successful, false if not.</returns>
        bool SetAuthenticatedUserForRequest();
    }
}
