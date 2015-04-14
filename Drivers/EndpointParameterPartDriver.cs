using CSM.WebApi.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Drivers
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EndpointParameterPartDriver : ContentPartDriver<EndpointParameterPart>
    {
        protected override string Prefix
        {
            get { return "EndpointParameter"; }
        }

        protected override DriverResult Display(EntityFieldPart part, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape("Parts_EndpointParameter", () => shapeHelper.Parts_EndpointParameter()),
                ContentShape("Parts_EndpointParameter_Summary", () => shapeHelper.Parts_EndpointParameter_Summary()),
                ContentShape("Parts_EndpointParameter_SummaryAdmin", () => shapeHelper.Parts_EndpointParameter_SummaryAdmin())
            );
        }

        protected override DriverResult Editor(EntityFieldPart part, dynamic shapeHelper)
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

        protected override DriverResult Editor(EntityFieldPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}
