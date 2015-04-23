using CSM.WebApi.Models;
using CSM.WebApi.Services;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Tokens;

namespace CSM.WebApi.Tokens
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EndpointTokensProvider : DocumentationTokensProvider<EndpointPart>, ITokenProvider
    {
        public EndpointTokensProvider(IContentManager contentManager, ITokenEncoder tokenEncoder)
            : base(contentManager, tokenEncoder)
        {
        }

        public void Describe(DescribeContext context)
        {
            var describe = context.For("Endpoints", T("Endpoints"), T("Tokens for Endpoints"));
            DescribeCurrentTokens(describe);
            DescribeAutoTokens(describe);
        }

        public void Evaluate(EvaluateContext context)
        {
            var evaluate = context.For<EndpointPart>("Endpoints", GetCurrentPart(context));
            EvaluateCurrentTokens(evaluate);
            EvaluateAutoTokens(evaluate);
        }
    }
}
