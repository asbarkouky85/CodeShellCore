using System.Globalization;
using System.Linq;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Web.Moldster;
using CodeShellCore.Web.Moldster.Configurator;
using CodeShellCore.Web;

namespace Configurator.UI
{
    public class UIShell : MoldsterShell
    {
        protected override bool useLocalization => false;

        protected override CultureInfo defaultCulture => new CultureInfo("en");
        public UIShell(IConfiguration config) : base(config)
        {
        }

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            coll.AddModsterSecurity();
        }
        
        protected override void OnReady()
        {
            base.OnReady();
            var conf = UISessionManager.LoadAppsFromConfig();
            ConfiguratorServerConfig.SetApps(conf.AppList.Select(d => new AppInfo
            {
                Name = d.Name,
                ConfigUrl = d.ConfigUrl
            }).ToArray());
            
        }

    }
}
