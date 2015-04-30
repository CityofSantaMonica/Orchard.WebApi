using Orchard;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Services
{
    public interface ITokenEncoder : IDependency
    {
        TokenEncodingContext Encode(string token, string prefix);
        TokenEncodingContext Decode(string token);
    }

    [OrchardFeature("CSM.WebApi.Documentation")]
    public class TokenEncodingContext
    {
        public string Token { get; set; }
        public string Prefix { get; set; }
        public string SubToken { get; set; }
    }
}
