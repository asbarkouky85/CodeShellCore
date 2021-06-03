using CodeShellCore.Moldster;
using CodeShellCore.Web.Razor.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace CodeShellCore.Web.Razor
{
    public class MoldsterWebShell : WebShell
    {

        public MoldsterWebShell(IConfiguration config) : base(config)
        {
        }

        protected override bool UseCors => true;
        protected virtual bool MigrateOnStartup => true;
        protected override bool useLocalization => false;
        protected override string DefaultCorsOrigins => "http://localhost:8050,http://localhost:4200,http://127.0.0.1:8050";

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

        public override void RegisterEnpointRoutes(IEndpointRouteBuilder endpoint)
        {
            endpoint.MapHub<GenerationHub>("/generationHub");
            endpoint.MapHub<TasksHub>("/tasksHub");
        }

        protected override void OnReady()
        {
            base.OnReady();
            this.ConfigureAngular2Razor();

            if (MigrateOnStartup)
            {
                using (var sc = GetScope())
                {
                    var molds = sc.ServiceProvider.GetRequiredService<MoldsterContext>();
                    molds.Database.Migrate();
                }
            }

        }
    }
}
