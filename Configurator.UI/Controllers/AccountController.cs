using CodeShellCore.Security;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Moldster.Configurator;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Configurator.UI.Controllers
{
    public class AccountController : AccountControllerBase
    {
        public List<AppInfo> GetApps()
        {
            var c = new ConfiguratorServerConfig();
            return c.Apps.ToList();
        }
    }
}
