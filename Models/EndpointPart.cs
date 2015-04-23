using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Models
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EndpointPartRecord : ContentPartRecord
    {
        public virtual string SelectedEntityId { get; set; }
        public virtual string SelectedErrorIds { get; set; }
        public virtual string SelectedParameterIds { get; set; }
    }

    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EndpointPart : ContentPart<EndpointPartRecord>
    {
        [Required]
        public string ApiPath
        {
            get { return this.Retrieve(x => x.ApiPath); }
            set { this.Store(x => x.ApiPath, value); }
        }

        [Required]
        public string Verb
        {
            get { return this.Retrieve(x => x.Verb); }
            set { this.Store(x => x.Verb, value); }
        }

        [Required]
        public string Description
        {
            get { return this.Retrieve(x => x.Description); }
            set { this.Store(x => x.Description, value); }
        }
    }
}
