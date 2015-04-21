using CSM.WebApi.Models;
using CSM.WebApi.Services;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Drivers
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class ErrorResultPartDriver : ContentPartDriver<ErrorResultPart>
    {
        private readonly IDocumentationService _documentationService;

        public ErrorResultPartDriver(IDocumentationService documentationService)
        {
            _documentationService = documentationService;
        }

        protected override string Prefix
        {
            get { return "ErrorResult"; }
        }

        protected override DriverResult Display(ErrorResultPart part, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape("Parts_ErrorResult", () => shapeHelper.Parts_ErrorResult(ViewModel:_documentationService.ToViewModel(part))),
                ContentShape("Parts_ErrorResult_Summary", () => shapeHelper.Parts_ErrorResult_Summary(ViewModel: _documentationService.ToViewModel(part))),
                ContentShape("Parts_ErrorResult_SummaryAdmin", () => shapeHelper.Parts_ErrorResult_SummaryAdmin())
            );
        }

        protected override DriverResult Editor(ErrorResultPart part, dynamic shapeHelper)
        {
            return ContentShape(
                "Parts_ErrorResult_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts.ErrorResult",
                    Model: part,
                    Prefix: Prefix
                )
            );
        }

        protected override DriverResult Editor(ErrorResultPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override void Exporting(ErrorResultPart part, ExportContentContext context)
        {
            ExportInfoset(part, context);
        }

        protected override void Importing(ErrorResultPart part, ImportContentContext context)
        {
            ImportInfoset(part, context);
        }
    }
}
