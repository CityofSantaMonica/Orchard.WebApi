using Orchard.Security;

namespace CSM.Security.Services
{
    public static class AuthenticationExtensions
    {
        public static bool IsAuthenticated(this IAuthenticationService service)
        {
            return service.GetAuthenticatedUser() != null;
        }
    }
}