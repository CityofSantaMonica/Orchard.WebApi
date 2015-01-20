using System;
using System.Linq;
using System.Web.Http.Filters;
using CSM.Security.Models;

namespace CSM.Security.Filters
{
    /// <summary>
    /// Action filter attribute to enable the decorated controller/action(s) to be accessed via cross-domain javascript.
    /// </summary>
    public class EnableCorsAttribute : ActionFilterAttribute
    {
        private readonly CorsPolicy _corsPolicy;

        public EnableCorsAttribute()
            : this("*", "*")
        {
        }

        /// <summary>
        /// Initialize a new instance of the EnableCorsAttribute.
        /// </summary>
        /// <param name="origins">A comma separated list of origin domains (or * to allow any origin)</param>
        /// <param name="methods">A comma separated list of allowed HTTP methods (or * to allow any method)</param>
        public EnableCorsAttribute(string origins, string methods)
        {
            _corsPolicy = new CorsPolicy();

            if (origins == "*")
                _corsPolicy.AllowAnyOrigin = true;

            if (methods == "*")
                _corsPolicy.AllowAnyMethod = true;

            string[] methodsArray = methods.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string[] originsArray = origins.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            _corsPolicy.AllowedOrigins = originsArray;
            _corsPolicy.AllowedMethods = methodsArray;
        }

        /// <summary>
        /// Applies the configured CORS policy to the executed action context.
        /// </summary>
        public override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            var request = actionContext.Request;
            var response = actionContext.Response;

            if (_corsPolicy.AllowAnyOrigin && _corsPolicy.AllowAnyMethod)
            {
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                return;
            }

            if(!request.Headers.Contains("Origin"))
                return;

            var requestOrigin = request.Headers.GetValues("Origin").FirstOrDefault();
            var requestMethod = request.Method.Method;

            if (!String.IsNullOrEmpty(requestOrigin))
            {
                bool originAllowed = 
                    _corsPolicy.AllowAnyOrigin ||
                    _corsPolicy.AllowedOrigins.Any(o => o.Equals(requestOrigin, StringComparison.OrdinalIgnoreCase));

                bool methodAllowed =
                    _corsPolicy.AllowAnyMethod ||
                    _corsPolicy.AllowedMethods.Any(m => m.Equals(requestMethod, StringComparison.OrdinalIgnoreCase));

                if (originAllowed && methodAllowed)
                    response.Headers.Add("Access-Control-Allow-Origin", new[] { requestOrigin });
            }

            base.OnActionExecuted(actionContext);
        }
    }
}