using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Moldster;
using CodeShellCore.Web.Moldster.Configurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Configurator.UI.Controllers
{
    public class HomeController : MoldsterUIController
    {
        public override string DefaultDomain => "ClientApp";
        public override IServerConfig ServerConfig => new ConfiguratorServerConfig();
        public override string GetDefaultTitle(string loc)
        {
            return "Configurator";
        }
    }
}
