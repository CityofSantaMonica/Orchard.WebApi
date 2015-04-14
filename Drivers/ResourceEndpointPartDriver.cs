using CSM.WebApi.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Drivers
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class ResourceEndpointPartDriver : ContentPartDriver<ResourceEndpointPart>
    {
        protected override string Prefix
        {
            get { return "ResourceEndpoint"; }
        }

        protected override DriverResult Display(EntityFieldPart part, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape("Parts_ResourceEndpoint", () => shapeHelper.Parts_ResourceEndpoint()),
                ContentShape("Parts_ResourceEndpoint_Summary", () => shapeHelper.Parts_ResourceEndpoint_Summary()),
                ContentShape("Parts_ResourceEndpoint_SummaryAdmin", () => shapeHelper.Parts_ResourceEndpoint_SummaryAdmin())
            );
        }

        protected override DriverResult Editor(EntityFieldPart part, dynamic shapeHelper)
        {
            return ContentShape(
                "Parts_ResourceEndpoint_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts.ResourceEndpoint",
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
