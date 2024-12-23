using CodeShellCore.Http.Pushing;
using CodeShellCore.Web.Security;
using System;

namespace CodeShellCore.Web.Razor.Services
{
    public class ConfigSessionManager : TokenSessionManager, IPushingSessionManager
    {
        public ConfigSessionManager(IServiceProvider prov) : base(prov)
        {
        }
    }
}
