using CSM.WebApi.Models;
using CSM.WebApi.Services;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Tokens;

namespace CSM.WebApi.Tokens
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EntityDefinitionTokensProvider : DocumentationTokensProvider<EntityDefinitionPart>, ITokenProvider
    {
        public EntityDefinitionTokensProvider(IContentManager contentManager, ITokenEncoder tokenEncoder)
            : base(contentManager, tokenEncoder)
        {
        }

        public void Describe(DescribeContext context)
        {
            var describe = context.For("EntityDefinitions", T("EntityDefinitions"), T("Tokens for EntityDefinitions"));
            DescribeCurrentTokens(describe);
            DescribeAutoTokens(describe);
        }

        public void Evaluate(EvaluateContext context)
        {
            var evaluate = context.For<EntityDefinitionPart>("EntityDefinitions", GetCurrentPart(context));
            EvaluateCurrentTokens(evaluate);
            EvaluateAutoTokens(evaluate);
        }
    }
}
