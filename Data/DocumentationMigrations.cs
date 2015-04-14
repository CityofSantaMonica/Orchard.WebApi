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
                .DisplayedAs("Entity Field")
                .WithPart("CommonPart", part => part
                    .WithSetting("DateEditorSettings.ShowDateEditor", "False")
                    .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "False")
                )
                .WithPart("IdentityPart")
                .WithPart("TitlePart")
                .WithPart("EntityFieldPart")
            );

            ContentDefinitionManager.AlterPartDefinition("ErrorResultPart", part => part
                .Attachable(false)
            );

            ContentDefinitionManager.AlterTypeDefinition("ErrorResult", type => type
                .DisplayedAs("Error Result")
                .WithPart("CommonPart", part => part
                    .WithSetting("DateEditorSettings.ShowDateEditor", "False")
                    .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "False")
                )
                .WithPart("IdentityPart")
                .WithPart("TitlePart")
                .WithPart("ErrorResultPart")
            );

            ContentDefinitionManager.AlterPartDefinition("ResourceEndpointParameterPart", part => part
                .Attachable(false)
            );

            ContentDefinitionManager.AlterTypeDefinition("ResourceEndpointParameter", type => type
                .DisplayedAs("Resource Endpoint Parameter")
                .WithPart("CommonPart", part => part
                    .WithSetting("DateEditorSettings.ShowDateEditor", "False")
                    .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "False")
                )
                .WithPart("IdentityPart")
                .WithPart("TitlePart")
                .WithPart("ResourceEndpointParameterPart")
            );

            ContentDefinitionManager.AlterPartDefinition("ResourceEndpointPart", part => part
                .Attachable(false)
                .WithField("Returns", field => field
                    .OfType("ContentPickerField")
                    .WithDisplayName("Returns")
                    .WithSetting("ContentPickerFieldSettings.Required", "True")
                    .WithSetting("ContentPickerFieldSettings.Multiple", "True")
                    .WithSetting("ContentPickerFieldSettings.ShowContentTab", "True")
                    .WithSetting("ContentPickerFieldSettings.DisplayedContentTypes", "EntityDefinition,ErrorResult")
                )
                .WithField("Parameters", field => field
                    .OfType("ContentPickerField")
                    .WithDisplayName("Parameters")
                    .WithSetting("ContentPickerFieldSettings.Required", "True")
                    .WithSetting("ContentPickerFieldSettings.Multiple", "True")
                    .WithSetting("ContentPickerFieldSettings.ShowContentTab", "True")
                    .WithSetting("ContentPickerFieldSettings.DisplayedContentTypes", "ResourceEndpointParameter")
                )
            );

            ContentDefinitionManager.AlterTypeDefinition("ResourceEndpoint", type => type
                .DisplayedAs("Resource Endpoint")
                .WithPart("CommonPart", part => part
                    .WithSetting("DateEditorSettings.ShowDateEditor", "False")
                    .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "False")
                )
                .WithPart("IdentityPart")
                .WithPart("TitlePart")
                .WithPart("ResourceEndpointPart")
            );

            return 1;
        }
    }
}
