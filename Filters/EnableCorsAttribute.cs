using System;
using System.Linq;
using System.Web.Http.Filters;
using CSM.Security.Services;

namespace CSM.Security.Filters
{
    /// <summary>
    /// Filter to enable the decorated controller/action(s) to be accessed via cross-domain javascript.
    /// </summary>
    public class EnableCorsAttribute : ActionFilterAttribute
    {
        private readonly ICorsService _corsService;

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
            _corsService = new CorsServiceImpl(origins, methods);
        }

        /// <summary>
        /// Applies the configured CORS policy to the executed action context.
        /// </summary>
        public override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            var request = actionContext.Request;
            var response = actionContext.Response;
            var requestMethod = request.Method.Method;

            if (_corsService.AllowsAnyOrigin() && _corsService.AllowsMethod(requestMethod))
            {
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                return;
            }

            var requestOrigin = request.Headers.GetValues("Origin").FirstOrDefault();

            if (!String.IsNullOrEmpty(requestOrigin))
            {
                if (_corsService.AllowsOrigin(requestOrigin) &&
                    _corsService.AllowsMethod(requestMethod))
                {
                    response.Headers.Add("Access-Control-Allow-Origin", new[] { requestOrigin });
                }
            }
        }
    }
}