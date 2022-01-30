using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeShellCore.Http.Pushing;
using CodeShellCore.Moldster.Configurator;
using Microsoft.AspNetCore.Http;

namespace CodeShellCore.Web.Razor.Services
{
    public class ConfigSessionManager : CodeShellCore.Web.Security.TokenSessionManager, IPushingSessionManager
    {
        public ConfigSessionManager(IServiceProvider prov) : base(prov)
        {
        }
    }
}
