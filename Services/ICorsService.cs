using Orchard;

namespace CSM.Security.Services
{
    public interface ICorsService : IDependency
    {
        bool AllowsAnyOrigin();
        bool AllowsOrigin(string origin);
        bool AllowsAnyMethod();
        bool AllowsMethod(string method);
    }
}