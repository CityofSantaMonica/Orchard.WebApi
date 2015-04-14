using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.ContentPicker.Fields;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Models
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class ResourceEndpointPart : ContentPart
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

        public IEnumerable<EntityDefinitionPart> ReturnsEntityDefinitionParts
        {
            get
            {
                var field = getContentPicker("Returns");
                if (field != null)
                {
                    return field.ContentItems.Select(item => item.As<EntityDefinitionPart>()).Where(e => e != null);
                }
                return Enumerable.Empty<EntityDefinitionPart>();
            }
        }

        public IEnumerable<ErrorResultPart> ReturnsErrorResultParts
        {
            get
            {
                var field = getContentPicker("Returns");
                if (field != null)
                {
                    return field.ContentItems.Select(item => item.As<ErrorResultPart>()).Where(e => e != null);
                }
                return Enumerable.Empty<ErrorResultPart>();
            }
        }

        public IEnumerable<ResourceEndpointParameterPart> ResourceEndpointParameterParts
        {
            get
            {
                var field = getContentPicker("Parameters");
                if (field != null)
                {
                    return field.ContentItems.Select(item => item.As<ResourceEndpointParameterPart>());
                }
                return Enumerable.Empty<ResourceEndpointParameterPart>();
            }
        }

        private ContentPickerField getContentPicker(string name)
        {
            return this.Get(typeof(ContentPickerField), name) as ContentPickerField;
        }
    }
}
