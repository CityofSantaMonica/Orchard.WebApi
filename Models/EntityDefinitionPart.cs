using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
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
    }
}
