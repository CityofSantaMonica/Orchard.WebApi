using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.ContentPicker.Fields;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.ViewModels
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class Endpoint
    {
        public string Verb { get; set; }

        public string ApiPath { get; set; }

        public IEnumerable<EntityDefinition> ReturnsEntityDefinitions { get; set; }

        public IEnumerable<ErrorResult> ReturnsErrorResults { get; set; }

        public IEnumerable<EndpointParameter> EndpointParameters { get; set; }
    }
}
