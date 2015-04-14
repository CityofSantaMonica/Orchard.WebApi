using System.Collections.Generic;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.ViewModels
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EntityDefinition
    {
        public string ApiName { get; set; }

        public string Description { get; set; }

        public IEnumerable<EntityField> EntityFields { get; set; }
    }
}
