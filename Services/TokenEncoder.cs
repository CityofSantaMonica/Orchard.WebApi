using System;
using Orchard;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Services
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class TokenEncoder : ITokenEncoder
    {
        private const string separator = ":";

        public TokenEncodingContext Encode(string token, string prefix = null)
        {
            if (String.IsNullOrEmpty(prefix))
                return new TokenEncodingContext { Token = token };
            else
            {
                string encoded = String.Format("{0}{1}{2}", prefix, separator, token);
                return new TokenEncodingContext { Token = encoded, Prefix = prefix, SubToken = token };
            }
        }

        public TokenEncodingContext Decode(string token)
        {
            string[] tokenParts = token.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            if (tokenParts.Length == 2)
                return new TokenEncodingContext { Token = token, Prefix = tokenParts[0], SubToken = tokenParts[1] };
            else
                return null;
        }
    }
}
