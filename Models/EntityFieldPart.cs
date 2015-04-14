using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Models
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EntityFieldPart : ContentPart
    {
        [Required]
        public string ApiName
        {
            get { return this.Retrieve(x => x.ApiName); }
            set { this.Store(x => x.ApiName, value); }
        }

        [Required]
        public string DataType
        {
            get { return this.Retrieve(x => x.DataType); }
            set { this.Store(x => x.DataType, value); }
        }
    }
}
