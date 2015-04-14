using CSM.WebApi.Models;
using CSM.WebApi.ViewModels;
using Orchard;

namespace CSM.WebApi.Services
{
    public interface IDocumentationService : IDependency
    {
        Endpoint ToViewModel(EndpointPart part);
        EndpointParameter ToViewModel(EndpointParameterPart part);
        EntityDefinition ToViewModel(EntityDefinitionPart part);
        EntityField ToViewModel(EntityFieldPart part);
        ErrorResult ToViewModel(ErrorResultPart part);
    }
}
