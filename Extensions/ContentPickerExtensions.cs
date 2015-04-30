using System;
using System.Collections.Generic;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.ContentPicker.Fields;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Extensions
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public static class ContentPickerExtensions
    {
        internal static ContentPickerField GetContentPicker(this ContentPart part, string fieldName)
        {
            return part.Get(typeof(ContentPickerField), fieldName) as ContentPickerField;
        }

        internal static IEnumerable<T> GetPickedContentAs<T>(this ContentPickerField picker) where T : IContent
        {
            if (picker != null && picker.ContentItems != null && picker.ContentItems.Any())
            {
                return picker.ContentItems.Select(item => item.As<T>()).Where(t => t != null);
            }

            return Enumerable.Empty<T>();
        }

        internal static string SerializeIds(this ContentPickerField picker)
        {
            if (picker == null)
                return String.Empty;

            return picker.Ids.SerializeIds();
        }

        internal static string SerializeIds(this IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
                return String.Empty;

            return String.Join(",", ids.OrderBy(i => i).Select(i => i.SerializeId()));
        }

        internal static string SerializeId(this int id)
        {
            return String.Format("{{{0}}}", id);
        }
    }
}
