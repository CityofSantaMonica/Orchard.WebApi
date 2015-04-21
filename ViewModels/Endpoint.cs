using System;
using System.Collections.Generic;
using Orchard.Environment.Extensions;

namespace CSM.WebApi.ViewModels
{
    [OrchardFeature("CSM.WebApi.Documentation")]
    public class Endpoint
    {
        public string Title { get; set; }

        public string Verb { get; set; }

        public string ApiPath { get; set; }

        public string Heading
        {
            get { return String.Format("{0} {1}", Verb, ApiPath); }
        }

        public IEnumerable<EndpointParameter> Parameters { get; set; }

        public EntityDefinition Returns { get; set; }

        public string ReturnsAmount { get; set; }

        public IEnumerable<ErrorResult> Errors { get; set; }
    }
}
