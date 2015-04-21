using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Data
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class DocumentationMigrations : DataMigrationImpl
    {
        public int Create()
        {
            ContentDefinitionManager.AlterPartDefinition("EndpointParameterPart", part => part
                .Attachable(false)
            );

            ContentDefinitionManager.AlterTypeDefinition("EndpointParameter", type => type
                .Creatable()
                .Draftable()
                .Listable()
                .DisplayedAs("Endpoint Parameter")
                .WithPart("CommonPart", part => part
                    .WithSetting("DateEditorSettings.ShowDateEditor", "False")
                    .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "False")
                )
                .WithPart("IdentityPart")
                .WithPart("TitlePart")
                .WithPart("BodyPart")
                .WithPart("EndpointParameterPart")
            );

            ContentDefinitionManager.AlterPartDefinition("EndpointPart", part => part
                .Attachable(false)
                .WithField("Parameters", field => field
                    .OfType("ContentPickerField")
                    .WithDisplayName("Parameters")
                    .WithSetting("ContentPickerFieldSettings.Required", "False")
                    .WithSetting("ContentPickerFieldSettings.Multiple", "True")
                    .WithSetting("ContentPickerFieldSettings.ShowContentTab", "True")
                    .WithSetting("ContentPickerFieldSettings.DisplayedContentTypes", "EndpointParameter")
                )
                .WithField("Returns", field => field
                    .OfType("ContentPickerField")
                    .WithDisplayName("Returns")
                    .WithSetting("ContentPickerFieldSettings.Required", "True")
                    .WithSetting("ContentPickerFieldSettings.Multiple", "False")
                    .WithSetting("ContentPickerFieldSettings.ShowContentTab", "True")
                    .WithSetting("ContentPickerFieldSettings.DisplayedContentTypes", "EntityDefinition")
                )
                .WithField("ReturnsAmount", field => field
                    .OfType("EnumerationField")
                    .WithDisplayName("Returns Amount")
                    .WithSetting("EnumerationFieldSettings.Required", "True")
                    .WithSetting("EnumerationFieldSettings.ListMode", "Radiobutton")
                    .WithSetting("EnumerationFieldSettings.Options", "Single\r\nCollection")
                )
                .WithField("Errors", field => field
                    .OfType("ContentPickerField")
                    .WithDisplayName("Errors")
                    .WithSetting("ContentPickerFieldSettings.Required", "False")
                    .WithSetting("ContentPickerFieldSettings.Multiple", "True")
                    .WithSetting("ContentPickerFieldSettings.ShowContentTab", "True")
                    .WithSetting("ContentPickerFieldSettings.DisplayedContentTypes", "ErrorResult")
                )
            );

            ContentDefinitionManager.AlterTypeDefinition("Endpoint", type => type
                .Creatable()
                .Draftable()
                .Listable()
                .DisplayedAs("Endpoint")
                .WithPart("CommonPart", part => part
                    .WithSetting("DateEditorSettings.ShowDateEditor", "False")
                    .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "False")
                )
                .WithPart("IdentityPart")
                .WithPart("TitlePart")
                .WithPart("EndpointPart")
            );

            ContentDefinitionManager.AlterPartDefinition("EntityDefinitionPart", part => part
                .Attachable(false)
                .WithField("FieldDefinitions", field => field
                    .OfType("ContentPickerField")
                    .WithDisplayName("Field Definitions")
                    .WithSetting("ContentPickerFieldSettings.Required", "True")
                    .WithSetting("ContentPickerFieldSettings.Multiple", "True")
                    .WithSetting("ContentPickerFieldSettings.ShowContentTab", "True")
                    .WithSetting("ContentPickerFieldSettings.DisplayedContentTypes", "EntityField")
                )
            );

            ContentDefinitionManager.AlterTypeDefinition("EntityDefinition", type => type
                .Creatable()
                .Draftable()
                .Listable()
                .DisplayedAs("Entity Definition")
                .WithPart("CommonPart", part => part
                    .WithSetting("DateEditorSettings.ShowDateEditor", "False")
                    .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "False")
                )
                .WithPart("IdentityPart")
                .WithPart("TitlePart")
                .WithPart("BodyPart")
                .WithPart("EntityDefinitionPart")
            );

            ContentDefinitionManager.AlterPartDefinition("EntityFieldPart", part => part
                .Attachable(false)
            );

            ContentDefinitionManager.AlterTypeDefinition("EntityField", type => type
                .Creatable()
                .Draftable()
                .Listable()
                .DisplayedAs("Entity Field")
                .WithPart("CommonPart", part => part
                    .WithSetting("DateEditorSettings.ShowDateEditor", "False")
                    .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "False")
                )
                .WithPart("IdentityPart")
                .WithPart("TitlePart")
                .WithPart("BodyPart")
                .WithPart("EntityFieldPart")
            );

            ContentDefinitionManager.AlterPartDefinition("ErrorResultPart", part => part
                .Attachable(false)
            );

            ContentDefinitionManager.AlterTypeDefinition("ErrorResult", type => type
                .Creatable()
                .Draftable()
                .Listable()
                .DisplayedAs("Error Result")
                .WithPart("CommonPart", part => part
                    .WithSetting("DateEditorSettings.ShowDateEditor", "False")
                    .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "False")
                )
                .WithPart("IdentityPart")
                .WithPart("TitlePart")
                .WithPart("BodyPart")
                .WithPart("ErrorResultPart")
            );

            return 1;
        }
    }
}
