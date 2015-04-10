using Orchard.Environment.Extensions;

namespace CSM.WebApi
{
    [OrchardFeature("CSM.WebApi.Security")]
    public class BasicAuthenticationCredentials
    {
        public string Username { get; internal set; }
        internal string Password { get; set; }
    }
}
