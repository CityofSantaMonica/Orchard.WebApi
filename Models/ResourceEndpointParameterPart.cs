using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Models
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class ResourceEndpointParameterPart : ContentPart
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

        [Required]
        public string Description
        {
            get { return this.Retrieve(x => x.Description); }
            set { this.Store(x => x.Description, value); }
        }

        public string Example
        {
            get { return this.Retrieve(x => x.Example); }
            set { this.Store(x => x.Example, value); }
        }

        [Required]
        public bool Required
        {
            get { return this.Retrieve(x => x.Required); }
            set { this.Store(x => x.Required, value); }
        }
    }
}
