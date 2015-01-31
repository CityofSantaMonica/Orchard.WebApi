namespace CSM.Security.Models
{
    public class BasicAuthenticationCredentials
    {
        public string Username { get; internal set; }
        internal string Password { get; set; }
    }
}