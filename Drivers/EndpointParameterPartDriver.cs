using CSM.WebApi.Models;
using CSM.WebApi.Services;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Drivers
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EndpointParameterPartDriver : ContentPartDriver<EndpointParameterPart>
    {
        private readonly IDocumentationService _documentationService;

        public EndpointParameterPartDriver(IDocumentationService documentationService)
        {
            _documentationService = documentationService;
        }

        protected override string Prefix
        {
            get { return "EndpointParameter"; }
        }

        protected override DriverResult Display(EndpointParameterPart part, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape("Parts_EndpointParameter", () => shapeHelper.Parts_EndpointParameter(EndpointParameter:  _documentationService.ToViewModel(part))),
                ContentShape("Parts_EndpointParameter_SummaryAdmin", () => shapeHelper.Parts_EndpointParameter_SummaryAdmin())
            );
        }

        protected override DriverResult Editor(EndpointParameterPart part, dynamic shapeHelper)
        {
            return ContentShape(
                "Parts_EndpointParameter_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts.EndpointParameter",
                    Model: part,
                    Prefix: Prefix
                )
            );
        }

        protected override DriverResult Editor(EndpointParameterPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override void Exporting(EndpointParameterPart part, ExportContentContext context)
        {
            ExportInfoset(part, context);
        }

        protected override void Importing(EndpointParameterPart part, ImportContentContext context)
        {
            ImportInfoset(part, context);
        }
    }
}
