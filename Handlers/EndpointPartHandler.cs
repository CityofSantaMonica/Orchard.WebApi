using System.Linq;
using CSM.WebApi.Extensions;
using CSM.WebApi.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Handlers
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EndpointPartHandler : ContentHandler
    {
        public EndpointPartHandler(IRepository<EndpointPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
            OnPublished<EndpointPart>(serializePickerFields);
        }

        private void serializePickerFields(PublishContentContext context, EndpointPart part)
        {
            part.Record.SelectedEntityId = part.GetContentPicker("Returns").Ids.SingleOrDefault().SerializeId();
            part.Record.SelectedErrorIds = part.GetContentPicker("Errors").SerializeIds();
            part.Record.SelectedParameterIds = part.GetContentPicker("Parameters").SerializeIds();
        }
    }
}
