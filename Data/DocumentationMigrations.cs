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
            ContentDefinitionManager.AlterPartDefinition(
                "ErrorResultPart",
                part => part
                    .Attachable(false)
                    .WithField(
                        "Code",
                        field => field
                            .OfType("NumericField")
                            .WithDisplayName("Code")
                            .WithSetting("NumericFieldSettings.Required", "True")
                            .WithSetting("NumericFieldSettings.Scale", "0")
                            .WithSetting("NumericFieldSettings.Minimum", "400")
                            .WithSetting("NumericFieldSettings.Maximum", "499")
                    )
                    .WithField(
                        "ReasonPhrase",
                        field => field
                            .OfType("TextField")
                            .WithDisplayName("Reason Phrase")
                            .WithSetting("TextFieldSettings.Required", "True")
                            .WithSetting("TextFieldSettings.Flavor", "Large")
                    )
                    .WithField(
                        "Explanation",
                        field => field
                            .OfType("TextField")
                            .WithDisplayName("Explanation")
                            .WithSetting("TextFieldSettings.Required", "True")
                            .WithSetting("TextFieldSettings.Flavor", "Html")
                    )
            );

            ContentDefinitionManager.AlterPartDefinition(
                "EntityFieldPart",
                part => part
                    .Attachable(false)
            );

            ContentDefinitionManager.AlterPartDefinition(
                "EntityDefinitionPart",
                part => part
                    .Attachable(false)
            );

            ContentDefinitionManager.AlterPartDefinition(
                "ResourceEndpointParameterPart",
                part => part
                    .Attachable(false)
            );

            ContentDefinitionManager.AlterPartDefinition(
                "ResourceEndpointPart",
                part => part
                    .Attachable(false)
            );

            return 1;
        }
    }
}
