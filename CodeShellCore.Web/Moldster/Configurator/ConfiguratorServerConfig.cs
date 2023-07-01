using CodeShellCore.Web.Moldster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Moldster.Configurator
{
    public class ConfiguratorServerConfig : DefaultServerConfig
    {
        static AppInfo[] _apps;
        public static void SetApps(AppInfo[] apps)
        {
            _apps = apps;
        }
        public AppInfo[] Apps { get { return _apps; } }
        
    }
}
