using CodeShellCore.Moldster;
using CodeShellCore.Web.Razor.SignalR;
using CodeShellCore.Web.Razor.Themes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;

namespace CodeShellCore.Web.Razor
{
    public class MoldsterWebShell : WebShell
    {
        protected virtual IRazorTheme Theme { get; }
        public MoldsterWebShell(IConfiguration config) : base(config)
        {
        }

        protected override bool UseCors => true;
        protected virtual bool MigrateOnStartup => true;
        protected override bool useLocalization => false;
        protected override string DefaultCorsOrigins => "http://localhost:8050,http://localhost:4200,http://127.0.0.1:8050,http://127.0.0.1:8051";

        protected override CultureInfo defaultCulture => new CultureInfo("en");

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);

            coll.AddMoldsterDbData(Configuration);
            coll.AddMoldsterWeb();
            coll.AddMoldsterConfigurator();
            coll.AddMoldsterServerGeneration();
            coll.AddMoldsterRazorHelpers();
            coll.AddRazorPages().AddRazorRuntimeCompilation();
            coll.Configure<MoldsterModuleOptions>(e =>
            {
                e.ReplaceComponentHtml = true;
                e.ReplaceComponentScripts = true;
                e.ReplaceDomainRoutes = true;
                e.ReplaceMainRoutes = true;
                e.ReplaceMainModule = true;
                e.ReplaceAppComponentHtml = true;
            });
            coll.AddOptions<MoldsterModuleOptions>();
        }

        public override void RegisterEndpointRoutes(IEndpointRouteBuilder endpoint)
        {
            base.RegisterEndpointRoutes(endpoint);
            endpoint.MapControllerRoute(
                name: "apiArea",
                pattern: "apiAction/{controller=Home}/{action=Index}/{id?}"
                );
            endpoint.AddMoldsterHubs();
        }

        protected override void OnReady()
        {
            base.OnReady();
            this.ConfigureAngular2Razor(Theme);
        }

        protected override void OnApplicationStarted(IServiceProvider prov)
        {
            base.OnApplicationStarted(prov);
            if (MigrateOnStartup)
                prov.MigrateContext<MoldsterContext>();
        }
    }
}
