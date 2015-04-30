using CSM.WebApi.Models;
using CSM.WebApi.Services;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Tokens;

namespace CSM.WebApi.Tokens
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EntityFieldTokensProvider : DocumentationTokensProvider<EntityFieldPart>, ITokenProvider
    {
        public EntityFieldTokensProvider(IContentManager contentManager, ITokenEncoder tokenEncoder)
            : base(contentManager, tokenEncoder)
        {
        }

        public void Describe(DescribeContext context)
        {
            var describe = context.For("EntityFields", T("EntityFields"), T("Tokens for EntityFields"));
            DescribeCurrentTokens(describe);
            DescribeAutoTokens(describe);
        }

        public void Evaluate(EvaluateContext context)
        {
            var evaluate = context.For<EntityFieldPart>("EntityFields", GetCurrentPart(context));
            EvaluateCurrentTokens(evaluate);
            EvaluateAutoTokens(evaluate);
        }
    }
}
