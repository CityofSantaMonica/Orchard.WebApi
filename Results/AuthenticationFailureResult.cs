using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Results
{
    [OrchardFeature("CSM.WebApi.Security")]
    public class AuthenticationFailureResult : IHttpActionResult
    {
        public string ReasonPhrase { get; private set; }
        public HttpRequestMessage Request { get; private set; }

        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(execute());
        }

        private HttpResponseMessage execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = Request;
            response.ReasonPhrase = ReasonPhrase;
            return response;
        }
    }
}
