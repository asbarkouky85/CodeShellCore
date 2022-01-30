using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using CodeShellCore.Text;
using CodeShellCore.Http.Pushing;
using System;

namespace CodeShellCore.Web.Moldster.Configurator
{
    public class UISessionManager : CodeShellCore.Web.Security.TokenSessionManager, IPushingSessionManager
    {
        
        static Dictionary<string, MoldsterAppData> _apps = new Dictionary<string, MoldsterAppData>();

        public UISessionManager(IServiceProvider prov) : base(prov)
        {
        }

        public override void AuthorizationRequest()
        {
            if (_accessor.HttpContext.Request.Headers.TryGetValue("app-name", out StringValues value))
            {
                
                if (_apps.TryGetValue(value.First(), out MoldsterAppData data))
                    _accessor.HttpContext.RequestServices.GetService<CurrentConfig>().App = data;
            }
            base.AuthorizationRequest();
        }

        public static AppsConfig LoadAppsFromConfig()
        {
            string cont = File.ReadAllText(Path.Combine(Shell.AppRootPath, "moldsterApps.json"));
            var appList = cont.FromJson<AppsConfig>();
            _apps = appList.AppList.ToDictionary(d => d.Name);
            return appList;
        }
    }
}
