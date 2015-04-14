using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.ContentPicker.Fields;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Models
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EntityDefinitionPart : ContentPart
    {
        [Required]
        public string ApiName
        {
            get { return this.Retrieve(x => x.ApiName); }
            set { this.Store(x => x.ApiName, value); }
        }

        public IEnumerable<EntityFieldPart> EntityFieldParts
        {
            get
            {
                var field = this.Get(typeof(ContentPickerField), "FieldDefinitions") as ContentPickerField;
                if (field != null)
                {
                    return field.ContentItems.Select(item => item.As<EntityFieldPart>());
                }
                return Enumerable.Empty<EntityFieldPart>();
            }
        }
    }
}
