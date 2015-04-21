using System.Linq;
using CSM.WebApi.Models;
using CSM.WebApi.ViewModels;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Environment.Extensions;
using Orchard.Fields.Fields;

namespace CSM.WebApi.Services
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class DocumentationService : IDocumentationService
    {
        public Endpoint ToViewModel(EndpointPart part)
        {
            var viewModel = new Endpoint() {
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
                Required = part.Required
            };

            return viewModel;
        }

        public EntityDefinition ToViewModel(EntityDefinitionPart part)
        {
            var viewModel = new EntityDefinition() {
                ApiName = part.ApiName,
                Description = part.As<BodyPart>().Text,
                EntityFields = part.GetContentPicker("FieldDefinitions")
                                   .GetPickedContentAs<EntityFieldPart>()
                                   .Select(field => ToViewModel(field))
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
                ReasonPhrase = part.ReasonPhrase
            };

            return viewModel;
        }
    }
}
