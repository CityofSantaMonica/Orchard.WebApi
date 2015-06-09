using System.Linq;
using System.Web.Http.Filters;

namespace CSM.WebApi.Filters
{
    public class EnableGlobalCorsAttribute : ActionFilterAttribute
    {
        private readonly string[] _methods;

        public EnableGlobalCorsAttribute(params string[] methods)
        {
            _methods = methods.Select(m => m.ToUpper()).ToArray();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null && corsApplies(actionExecutedContext))
            {
                actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            }

            base.OnActionExecuted(actionExecutedContext);
        }

        private bool corsApplies(HttpActionExecutedContext actionExecutedContext)
        {
            if (_methods != null && _methods.Any())
            {
                return _methods.Contains(actionExecutedContext.Request.Method.Method.ToUpper());
            }

            return true;
        }
    }
}
