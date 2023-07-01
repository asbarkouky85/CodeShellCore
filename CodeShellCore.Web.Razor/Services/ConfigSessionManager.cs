using CodeShellCore.Http.Pushing;
using System;

namespace CodeShellCore.Web.Razor.Services
{
    public class ConfigSessionManager : CodeShellCore.Web.Security.TokenSessionManager, IPushingSessionManager
    {
        public ConfigSessionManager(IServiceProvider prov) : base(prov)
        {
        }
    }
}
