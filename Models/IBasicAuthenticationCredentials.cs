using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Security.Models
{
    public interface IBasicAuthenticationCredentials
    {
        string Username { get; }
        string Password { get; }
    }
}
