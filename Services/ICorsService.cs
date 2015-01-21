using Orchard;

namespace CSM.Security.Services
{
    /// <summary>
    /// A service definition for implementing a CORS policy.
    /// </summary>
    public interface ICorsService : IDependency
    {
        /// <summary>
        /// True if any Origin header value satisfies the CORS policy, false otherwise.
        /// </summary>
        /// <remarks>
        /// This corresponds with a response header of Access-Control-Allow-Origin: *
        /// </remarks>
        bool AllowsAnyOrigin();

        /// <summary>
        /// True if the specified Origin header value satisfies the CORS policy, false otherwise.
        /// </summary>
        /// <param name="origin">The value of a request Origin header to check against the policy.</param>
        /// <remarks>
        /// This corresponds with a response header of Access-Control-Allow-Origin: <paramref name="origin"/>
        /// </remarks>
        bool AllowsOrigin(string origin);

        /// <summary>
        /// True if any HTTP method is allowed under the CORS policy, false otherwise.
        /// </summary>
        bool AllowsAnyMethod();

        /// <summary>
        /// True if the specified HTTP method is allowed under the CORS policy, false otherwise.
        /// </summary>
        /// <param name="method">An HTTP method (e.g. GET, POST) to check against the policy.</param>
        bool AllowsMethod(string method);
    }
}