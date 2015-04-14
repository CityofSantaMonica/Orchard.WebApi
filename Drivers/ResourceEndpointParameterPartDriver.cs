using CSM.WebApi.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Drivers
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class ResourceEndpointParameterPartDriver : ContentPartDriver<ResourceEndpointParameterPart>
    {
        protected override string Prefix
        {
            get { return "ResourceEndpointParameter"; }
        }

        protected override DriverResult Display(EntityFieldPart part, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape("Parts_ResourceEndpointParameter", () => shapeHelper.Parts_ResourceEndpointParameter()),
                ContentShape("Parts_ResourceEndpointParameter_Summary", () => shapeHelper.Parts_ResourceEndpointParameter_Summary()),
                ContentShape("Parts_ResourceEndpointParameter_SummaryAdmin", () => shapeHelper.Parts_ResourceEndpointParameter_SummaryAdmin())
            );
        }

        protected override DriverResult Editor(EntityFieldPart part, dynamic shapeHelper)
        {
            return ContentShape(
                "Parts_ResourceEndpointParameter_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts.ResourceEndpointParameter",
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
