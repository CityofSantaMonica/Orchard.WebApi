using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSM.Security.Models
{
    class BasicAuthenticationCredentials : IBasicAuthenticationCredentials
    {
        public string Username { get; internal set; }

        public string Password { get; internal set; }
    }
}