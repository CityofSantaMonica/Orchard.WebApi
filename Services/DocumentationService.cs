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

namespace CSM.WebApi.Services
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class DocumentationService : IDocumentationService
    {
        private readonly IContentManager _contentManager;

        public DocumentationService(IContentManager contentManager)
        {
            _contentManager = contentManager;
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
            var viewModel = new Endpoint() {
                Title = part.As<TitlePart>().Title,
                Verb = part.Verb,
                ApiPath = part.ApiPath,

                Parameters = part.GetContentPicker("Parameters")
                                 .GetPickedContentAs<EndpointParameterPart>()
                                 .Select(parameter => ToViewModel(parameter)),

                Returns = part.GetContentPicker("Returns")
                              .GetPickedContentAs<EntityDefinitionPart>()
                              .Select(entity => ToViewModel(entity))
                              .SingleOrDefault(),

                ReturnsAmount = ((EnumerationField)part.Get(typeof(EnumerationField), "ReturnsAmount")).Value,

                Errors = part.GetContentPicker("Errors")
                             .GetPickedContentAs<ErrorResultPart>()
                             .Select(error => ToViewModel(error))
            };

            return viewModel;
        }

        public EndpointParameter ToViewModel(EndpointParameterPart part)
        {
            var viewModel = new EndpointParameter() {
                ApiName = part.ApiName,
                DataType = part.DataType,
                Description = part.As<BodyPart>().Text,
                Example = part.Example,
                Required = part.Required,
                AssociatedEndpoints = GetAssociatedEndpoints(part).Select(ToViewModel)
            };

            return viewModel;
        }

        public EntityDefinition ToViewModel(EntityDefinitionPart part)
        {
            var viewModel = new EntityDefinition() {
                Title = part.As<TitlePart>().Title,
                ApiName = part.ApiName,
                Description = part.As<BodyPart>().Text,
                Fields = part.GetContentPicker("FieldDefinitions")
                                   .GetPickedContentAs<EntityFieldPart>()
                                   .Select(field => ToViewModel(field)),
                AssociatedEndpoints = GetAssociatedEndpoints(part).Select(ToViewModel)
            };

            return viewModel;
        }

        public EntityField ToViewModel(EntityFieldPart part)
        {
            var viewModel = new EntityField() {
                ApiName = part.ApiName,
                DataType = part.DataType,
                Description = part.As<BodyPart>().Text
            };

            return viewModel;
        }

        public ErrorResult ToViewModel(ErrorResultPart part)
        {
            var viewModel = new ErrorResult() {
                Code = part.Code.Value,
                Description = part.As<BodyPart>().Text,
                ReasonPhrase = part.ReasonPhrase,
                AssociatedEndpoints = GetAssociatedEndpoints(part).Select(ToViewModel)
            };

            return viewModel;
        }
    }
}
