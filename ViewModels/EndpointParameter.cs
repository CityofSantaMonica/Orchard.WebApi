using System.Collections.Generic;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.ViewModels
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EndpointParameter
    {
        public string ApiName { get; set; }

        public string DataType { get; set; }

        public string Description { get; set; }

        public string Example { get; set; }

        public bool Required { get; set; }

        public IEnumerable<Endpoint> AssociatedEndpoints { get; set; }
    }
}
