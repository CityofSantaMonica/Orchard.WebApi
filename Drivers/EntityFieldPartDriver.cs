using CSM.WebApi.Models;
using CSM.WebApi.Services;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Drivers
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EntityFieldPartDriver : ContentPartDriver<EntityFieldPart>
    {
        private readonly IDocumentationService _documentationService;

        public EntityFieldPartDriver(IDocumentationService documentationService)
        {
            _documentationService = documentationService;
        }

        protected override string Prefix
        {
            get { return "EntityField"; }
        }

        protected override DriverResult Display(EntityFieldPart part, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape("Parts_EntityField", () => shapeHelper.Parts_EntityField(EntityField: _documentationService.ToViewModel(part))),
                ContentShape("Parts_EntityField_SummaryAdmin", () => shapeHelper.Parts_EntityField_SummaryAdmin())
            );
        }

        protected override DriverResult Editor(EntityFieldPart part, dynamic shapeHelper)
        {
            return ContentShape(
                "Parts_EntityField_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts.EntityField",
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

        protected override void Exporting(EntityFieldPart part, ExportContentContext context)
        {
            ExportInfoset(part, context);
        }

        protected override void Importing(EntityFieldPart part, ImportContentContext context)
        {
            ImportInfoset(part, context);
        }
    }
}
