using CSM.WebApi.Models;
using CSM.WebApi.Services;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Tokens;

namespace CSM.WebApi.Tokens
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EndpointParameterTokensProvider : DocumentationTokensProvider<EndpointParameterPart>, ITokenProvider
    {
        public EndpointParameterTokensProvider(IContentManager contentManager, ITokenEncoder tokenEncoder)
            : base(contentManager, tokenEncoder)
        {
        }

        public void Describe(DescribeContext context)
        {
            var describe = context.For("EndpointParameters", T("EndpointParameters"), T("Tokens for EndpointParameters"));
            DescribeCurrentTokens(describe);
            DescribeAutoTokens(describe);
        }

        public void Evaluate(EvaluateContext context)
        {
            var evaluate = context.For<EndpointParameterPart>("EndpointParameters", GetCurrentPart(context));
            EvaluateCurrentTokens(evaluate);
            EvaluateAutoTokens(evaluate);
        }
    }
}
