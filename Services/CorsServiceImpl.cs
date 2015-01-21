using System;
using System.Collections.Generic;
using System.Linq;

namespace CSM.Security.Services
{
    class CorsService : ICorsService
    {
        private readonly bool _allowsAnyOrigin;
        private readonly bool _allowsAnyMethod;
        private readonly IEnumerable<string> _allowedOrigins;
        private readonly IEnumerable<string> _allowedMethods;

        public CorsService(string origins, string methods)
        {
            if (origins == "*")
                _allowsAnyOrigin = true;

            _allowedOrigins = origins.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            if (methods == "*")
                _allowsAnyMethod = true;

            _allowedMethods = methods.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }

        public bool AllowsAnyOrigin()
        {
            return _allowsAnyOrigin;
        }

        public bool AllowsOrigin(string origin)
        {
            return AllowsAnyOrigin() ||
                   _allowedOrigins.Any(o => o.Equals(origin, StringComparison.OrdinalIgnoreCase));
        }

        public bool AllowsAnyMethod()
        {
            return _allowsAnyMethod;
        }

        public bool AllowsMethod(string method)
        {
            return AllowsAnyMethod() ||
                   _allowedMethods.Any(m => m.Equals(method, StringComparison.OrdinalIgnoreCase));
        }
    }
}