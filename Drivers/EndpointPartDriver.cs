using CSM.WebApi.Models;
using CSM.WebApi.Services;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Drivers
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EndpointPartDriver : ContentPartDriver<EndpointPart>
    {
        private readonly IDocumentationService _documentationService;

        public EndpointPartDriver(IDocumentationService documentationService)
        {
            _documentationService = documentationService;
        }

        protected override string Prefix
        {
            get { return "Endpoint"; }
        }

        protected override DriverResult Display(EndpointPart part, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape(
                    "Parts_Endpoint",
                    () => shapeHelper.Parts_Endpoint(Endpoint: _documentationService.ToViewModel(part))
                ),
                ContentShape("Parts_Endpoint_SummaryAdmin", () => shapeHelper.Parts_Endpoint_SummaryAdmin())
            );
        }

        protected override DriverResult Editor(EndpointPart part, dynamic shapeHelper)
        {
            return ContentShape(
                "Parts_Endpoint_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts.Endpoint",
                    Model: part,
                    Prefix: Prefix
                )
            );
        }

        protected override DriverResult Editor(EndpointPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override void Exporting(EndpointPart part, ExportContentContext context)
        {
            ExportInfoset(part, context);
        }

        protected override void Importing(EndpointPart part, ImportContentContext context)
        {
            ImportInfoset(part, context);
        }
    }
}
