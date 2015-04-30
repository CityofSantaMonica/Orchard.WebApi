using Orchard.Environment.Extensions;

namespace CSM.WebApi.ViewModels
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class EntityField
    {
        public string ApiName { get; set; }

        public string DataType { get; set; }

        public string Description { get; set; }
    }
}
