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
        public int Code
        {
            get { return this.Retrieve(x => x.Code, 400); }
            set { this.Store(x => x.Code, value); }
        }

        [Required]
        public string ReasonPhrase
        {
            get { return this.Retrieve(x => x.ReasonPhrase); }
            set { this.Store(x => x.ReasonPhrase, value); }
        }

        [Required]
        public string Explanation
        {
            get { return this.Retrieve(x => x.Explanation); }
            set { this.Store(x => x.Explanation, value); }
        }
    }
}
