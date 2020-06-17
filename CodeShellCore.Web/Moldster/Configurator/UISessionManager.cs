using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using CodeShellCore.Text;
using CodeShellCore.Http.Pushing;

namespace CodeShellCore.Web.Moldster.Configurator
{
    public class UISessionManager : CodeShellCore.Web.Security.TokenSessionManager, IPushingSessionManager
    {
        private readonly IHttpContextAccessor context;
        static Dictionary<string, MoldsterAppData> _apps = new Dictionary<string, MoldsterAppData>();

        public UISessionManager(IHttpContextAccessor context) : base(context)
        {
            this.context = context;
            
        }

        public override void AuthorizationRequest()
        {
            if (context.HttpContext.Request.Headers.TryGetValue("app-name", out StringValues value))
            {
                
                if (_apps.TryGetValue(value.First(), out MoldsterAppData data))
                    context.HttpContext.RequestServices.GetService<CurrentConfig>().App = data;
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
