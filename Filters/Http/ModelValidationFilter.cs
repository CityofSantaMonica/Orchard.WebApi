using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Orchard.Logging;

namespace CSM.Security.Filters.Http
{
    public class ModelValidationFilter : ActionFilterAttribute
    {
        public ILogger Logger { get; set; }

        public ModelValidationFilter()
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