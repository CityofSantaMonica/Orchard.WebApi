using CSM.WebApi.Models;
using CSM.WebApi.Services;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Tokens;

namespace CSM.WebApi.Tokens
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class ErrorResultTokensProvider : DocumentationTokensProvider<ErrorResultPart>, ITokenProvider
    {
        public ErrorResultTokensProvider(IContentManager contentManager, ITokenEncoder tokenEncoder)
            : base(contentManager, tokenEncoder)
        {
        }

        public void Describe(DescribeContext context)
        {
            var describe = context.For("ErrorResults", T("ErrorResults"), T("Tokens for ErrorResults"));
            DescribeCurrentTokens(describe);
            DescribeAutoTokens(describe);
        }

        public void Evaluate(EvaluateContext context)
        {
            var evaluate = context.For<ErrorResultPart>("ErrorResults", GetCurrentPart(context));
            EvaluateCurrentTokens(evaluate);
            EvaluateAutoTokens(evaluate);
        }
    }
}