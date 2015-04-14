using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Models
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EndpointPart : ContentPart
    {
        [Required]
        public string Verb
        {
            get { return this.Retrieve(x => x.Verb); }
            set { this.Store(x => x.Verb, value); }
        }

        [Required]
        public string ApiPath
        {
            get { return this.Retrieve(x => x.ApiPath); }
            set { this.Store(x => x.ApiPath, value); }
        }
    }
}
