using System.Collections.Generic;
using CSM.WebApi.Models;
using CSM.WebApi.ViewModels;
using Orchard;
using Orchard.ContentManagement;

namespace CSM.WebApi.Services
{
    public interface IDocumentationService : IDependency
    {
        IEnumerable<EndpointPart> GetAssociatedEndpoints(EndpointParameterPart parameter);
        IEnumerable<EndpointPart> GetAssociatedEndpoints(EntityDefinitionPart entity);
        IEnumerable<EndpointPart> GetAssociatedEndpoints(ErrorResultPart error);

        Endpoint ToViewModel(EndpointPart part);
        EndpointParameter ToViewModel(EndpointParameterPart part);
        EntityDefinition ToViewModel(EntityDefinitionPart part);
        EntityField ToViewModel(EntityFieldPart part);
        ErrorResult ToViewModel(ErrorResultPart part);
    }
}
