using System;
using System.Linq;
using CSM.WebApi.Services;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.Core.Title.Models;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Tokens;

namespace CSM.WebApi.Tokens
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public abstract class DocumentationTokensProvider<TPart> where TPart : ContentPart
    {
        private readonly IContentManager _contentManager;
        private readonly ITokenEncoder _tokenEncoder;

        public DocumentationTokensProvider(IContentManager contentManager, ITokenEncoder tokenEncoder)
        {
            _contentManager = contentManager;
            _tokenEncoder = tokenEncoder;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected void DescribeCurrentTokens(DescribeFor describe)
        {
            string name = typeof(TPart).Name;

            describe
                .Token("Current", T("Current"), T("The current {0}", name))
                .Token("Current:*", T("Current:<value>"), T("The value (as String) from the current {0}'s InfoSet", name), "Current")
                ;
        }

        protected void EvaluateCurrentTokens(EvaluateFor<TPart> evaluate)
        {
            evaluate
                .Token("Current", part => part)
                .Token(
                    token => token.StartsWith("Current:", StringComparison.OrdinalIgnoreCase) ? token.Substring("Current:".Length) : null,
                    (token, part) => part == null ? null : part.Retrieve<string>(token)
                );
        }

        protected void DescribeAutoTokens(DescribeFor describe)
        {
            foreach (var part in _contentManager.Query<TPart>().List())
            {
                var titlePart = part.As<ITitleAspect>();
                if (titlePart == null)
                    continue;

                var encodingContext = _tokenEncoder.Encode("*", titlePart.Title);
                describe.Token(
                    encodingContext.Token,
                    T(encodingContext.Token.Replace("*", "<value>")),
                    T("A value (as String) from the {0} {1}'s InfoSet", titlePart.Title, typeof(TPart).Name)
                );
            }
        }

        protected void EvaluateAutoTokens(EvaluateFor<TPart> evaluate)
        {
            evaluate
                .Token((token, defaultData) => {
                    var tokenParts = _tokenEncoder.Decode(token);
                    if (tokenParts == null)
                        return null;

                    var part = GetByTitle(tokenParts.Prefix);
                    if (part == null)
                        return null;

                    return part.Retrieve<string>(tokenParts.SubToken);
                });
        }

        protected TPart GetByTitle(string title)
        {
            var parts = _contentManager.Query<TPart>().Join<TitlePartRecord>().Where(titleRecord => titleRecord.Title == title);

            if (parts == null || parts.Count() == 0)
                return default(TPart);

            return parts.Slice(1).Single();
        }

        protected static TPart GetCurrentPart(EvaluateContext context)
        {
            IContent current = context.Data["Content"] as IContent;
            TPart currentPart = current == null ? default(TPart) : current.As<TPart>();
            return currentPart;
        }
    }
}
