using System.Collections.Generic;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.ViewModels
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class ErrorResult
    {
        public int Code { get; set; }

        public string ReasonPhrase { get; set; }

        public string Description { get; set; }

        public IEnumerable<Endpoint> AssociatedEndpoints { get; set; }
    }
}
