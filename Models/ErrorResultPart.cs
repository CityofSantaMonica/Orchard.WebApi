using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Models
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class ErrorResultPart : ContentPart
    {
        [Required]
        [Range(400, 499)]
        public int? Code
        {
            get { return this.Retrieve(x => x.Code); }
            set { this.Store(x => x.Code, value); }
        }

        [Required]
        public string ReasonPhrase
        {
            get { return this.Retrieve(x => x.ReasonPhrase); }
            set { this.Store(x => x.ReasonPhrase, value); }
        }
    }
}
