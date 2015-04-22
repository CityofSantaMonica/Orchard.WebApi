using System.Collections.Generic;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.ViewModels
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EntityDefinition
    {
        public string Title { get; set; }

        public string ApiName { get; set; }

        public string Description { get; set; }

        public string Heading
        {
            get
            {
                return ApiName;
            }
        }

        public IEnumerable<EntityField> Fields { get; set; }

        public IEnumerable<Endpoint> AssociatedEndpoints { get; set; }
    }
}
