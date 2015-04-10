using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.Models
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class ErrorResultPart : ContentPart
    {
        public int Code
        {
            get { throw new NotImplementedException(); }
        }

        public string ReasonPhrase
        {
            get { throw new NotImplementedException(); }
        }

        public string Explanation
        {
            get { throw new NotImplementedException(); }
        }
    }
}
