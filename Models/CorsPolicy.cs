using System.Collections.Generic;

namespace CSM.Security.Models
{
    class CorsPolicy
    {
        public bool AllowAnyMethod { get; internal set; }

        public bool AllowAnyOrigin { get; internal set; }

        public IEnumerable<string> AllowedMethods { get; internal set; }

        public IEnumerable<string> AllowedOrigins { get; internal set; }
    }
}