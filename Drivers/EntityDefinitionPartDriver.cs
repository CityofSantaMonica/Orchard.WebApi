using CSM.WebApi.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Drivers
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EntityDefinitionPartDriver : ContentPartDriver<EntityDefinitionPart>
    {
        protected override string Prefix
        {
            get { return "EntityDefinition"; }
        }

        protected override DriverResult Display(EntityDefinitionPart part, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape("Parts_EntityDefinition", () => shapeHelper.Parts_EntityDefinition()),
                ContentShape("Parts_EntityDefinition_Summary", () => shapeHelper.Parts_EntityDefinition_Summary()),
                ContentShape("Parts_EntityDefinition_SummaryAdmin", () => shapeHelper.Parts_EntityDefinition_SummaryAdmin())
            );
        }

        protected override DriverResult Editor(EntityDefinitionPart part, dynamic shapeHelper)
        {
            return ContentShape(
                "Parts_EntityDefinition_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts.EntityDefinition",
                    Model: part,
                    Prefix: Prefix
                )
            );
        }

        protected override DriverResult Editor(EntityDefinitionPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}
