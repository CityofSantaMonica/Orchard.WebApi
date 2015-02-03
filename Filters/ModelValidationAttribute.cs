using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Orchard.Environment.Extensions;
using Orchard.Logging;

namespace CSM.WebApi.Filters
{
    /// <summary>
    /// Filter to check the incoming <see cref="ModelState"/> and set an ErrorResponse if invalid.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [OrchardFeature("CSM.WebApi.Security")]
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public ILogger Logger { get; set; }

        public ModelValidationAttribute()
        {
            Logger = NullLogger.Instance;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (!modelState.IsValid)
            {
                var request = actionContext.Request;

                Logger.Warning(
                    "Invalid model state {0} to {1}: [{2}]",
                    request.Method,
                    request.RequestUri,
                    String.Join(
                        ",",
                        modelState.SelectMany(state => state.Value.Errors)
                                  .Select(error => error.ErrorMessage)
                    )
                );

                actionContext.Response =
                    actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, modelState);
            }
        }
    }
}