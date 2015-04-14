using CSM.WebApi.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Drivers
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EndpointPartDriver : ContentPartDriver<EndpointPart>
    {
        protected override string Prefix
        {
            get { return "Endpoint"; }
        }

        protected override DriverResult Display(EndpointPart part, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape("Parts_Endpoint", () => shapeHelper.Parts_Endpoint()),
                ContentShape("Parts_Endpoint_Summary", () => shapeHelper.Parts_Endpoint_Summary()),
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
    }
}
