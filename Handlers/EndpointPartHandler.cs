using System;
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
            var returns = part.GetContentPicker("Returns");
            part.Record.SelectedEntityId = returns.Ids.First();

            var error = part.GetContentPicker("Errors");
            part.Record.SelectedErrorIds = serializeIds(error.Ids);

            var parameters = part.GetContentPicker("Parameters");
            part.Record.SelectedParameterIds = serializeIds(parameters.Ids);
        }

        private static string serializeIds(params int[] ids)
        {
            if (ids == null || !ids.Any())
                return String.Empty;

            //the call to String.Join is ambigious, cast is necessary
            return String.Join(",", (int[])ids);
        }
    }
}
