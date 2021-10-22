using System.Globalization;
using System.Linq;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Web.Moldster;
using CodeShellCore.Web.Moldster.Configurator;
using CodeShellCore.Web;
using CodeShellCore.DependencyInjection;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CodeShellCore.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Configurator.UI
{
    public class UIShell : WebShell
    {
        protected override bool useLocalization => false;
        protected override bool IsSpa => true;

        protected override CultureInfo defaultCulture => new CultureInfo("en");
        public UIShell(IConfiguration config) : base(config)
        {
        }

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            
            coll.AddScoped<ClientData>();
            coll.AddModsterSecurity();
        }

        public override void ConfigureHttp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            var originConfig = getConfig("AllowedOrigins").Value ?? "http://localhost:8050,http://127.0.0.1:8050,http://localhost:4200";

            app.UseCors(d => d.WithOrigins(originConfig.Split(","))
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());
            app.UseStaticFiles();
            base.ConfigureHttp(app, env);

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
