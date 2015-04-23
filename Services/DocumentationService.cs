using System.Collections.Generic;
using System.Linq;
using CSM.WebApi.Extensions;
using CSM.WebApi.Models;
using CSM.WebApi.ViewModels;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Core.Title.Models;
using Orchard.Environment.Extensions;
using Orchard.Fields.Fields;
using Orchard.Tokens;

namespace CSM.WebApi.Services
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class DocumentationService : IDocumentationService
    {
        private readonly IContentManager _contentManager;
        private readonly ITokenizer _tokenizer;

        public DocumentationService(IContentManager contentManager, ITokenizer tokenizer)
        {
            _contentManager = contentManager;
            _tokenizer = tokenizer;
        }

        public IEnumerable<EndpointPart> GetAssociatedEndpoints(EndpointParameterPart parameter)
        {
            return _contentManager
                    .Query<EndpointPart, EndpointPartRecord>(VersionOptions.Latest)
                    .Where(record => record.SelectedParameterIds.Contains(parameter.Id.SerializeId()))
                    .List();
        }

        public IEnumerable<EndpointPart> GetAssociatedEndpoints(EntityDefinitionPart entity)
        {
            return _contentManager
                    .Query<EndpointPart, EndpointPartRecord>(VersionOptions.Latest)
                    .Where(record => record.SelectedEntityId == entity.Id.SerializeId())
                    .List();
        }

        public IEnumerable<EndpointPart> GetAssociatedEndpoints(ErrorResultPart error)
        {
            return _contentManager
                    .Query<EndpointPart, EndpointPartRecord>(VersionOptions.Latest)
                    .Where(record => record.SelectedErrorIds.Contains(error.Id.SerializeId()))
                    .List();
        }

        public Endpoint ToViewModel(EndpointPart part)
        {
            var tokenState = new Dictionary<string, object> { { "Content", part.ContentItem } };

            var viewModel = new Endpoint() {
                ApiPath = _tokenizer.Replace(part.ApiPath, tokenState),
                Description = _tokenizer.Replace(part.Description, tokenState),
                Title = part.As<TitlePart>().Title,
                Verb = part.Verb,

                Parameters = part.GetContentPicker("Parameters")
                                 .GetPickedContentAs<EndpointParameterPart>()
                                 .Select(ToViewModel),

                Returns = part.GetContentPicker("Returns")
                              .GetPickedContentAs<EntityDefinitionPart>()
                              .Select(ToViewModel)
                              .SingleOrDefault(),

                ReturnsAmount = ((EnumerationField)part.Get(typeof(EnumerationField), "ReturnsAmount")).Value,

                Errors = part.GetContentPicker("Errors")
                             .GetPickedContentAs<ErrorResultPart>()
                             .Select(ToViewModel)
            };

            return viewModel;
        }

        public EndpointParameter ToViewModel(EndpointParameterPart part)
        {
            var tokenState = new Dictionary<string, object> { { "Content", part.ContentItem } };

            var viewModel = new EndpointParameter() {
                ApiName = part.ApiName,
                DataType = part.DataType,
                Description = _tokenizer.Replace(part.As<BodyPart>().Text, tokenState),
                Example = _tokenizer.Replace(part.Example, tokenState),
                Required = part.Required,
                AssociatedEndpoints = GetAssociatedEndpoints(part).Select(ToViewModel)
            };

            return viewModel;
        }

        public EntityDefinition ToViewModel(EntityDefinitionPart part)
        {
            var tokenState = new Dictionary<string, object> { { "Content", part.ContentItem } };

            var viewModel = new EntityDefinition() {
                Title = part.As<TitlePart>().Title,
                ApiName = part.ApiName,
                Description = _tokenizer.Replace(part.As<BodyPart>().Text, tokenState),
                Fields = part.GetContentPicker("FieldDefinitions")
                                   .GetPickedContentAs<EntityFieldPart>()
                                   .Select(ToViewModel),
                AssociatedEndpoints = GetAssociatedEndpoints(part).Select(ToViewModel)
            };

            return viewModel;
        }

        public EntityField ToViewModel(EntityFieldPart part)
        {
            var tokenState = new Dictionary<string, object> { { "Content", part.ContentItem } };

            var viewModel = new EntityField() {
                ApiName = part.ApiName,
                DataType = part.DataType,
                Description = _tokenizer.Replace(part.As<BodyPart>().Text, tokenState)
            };

            return viewModel;
        }

        public ErrorResult ToViewModel(ErrorResultPart part)
        {
            var tokenState = new Dictionary<string, object> { { "Content", part.ContentItem } };

            var viewModel = new ErrorResult() {
                Code = part.Code.Value,
                Description = _tokenizer.Replace(part.As<BodyPart>().Text, tokenState),
                ReasonPhrase = _tokenizer.Replace(part.ReasonPhrase, tokenState),
                AssociatedEndpoints = GetAssociatedEndpoints(part).Select(ToViewModel)
            };

            return viewModel;
        }
    }
}
