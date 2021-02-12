using CodeShellCore.Moldster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        protected virtual bool MigrateOnStartup => true;
        protected override bool useLocalization => false;

        protected override CultureInfo defaultCulture => new CultureInfo("en");

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);

            coll.AddMoldsterDbData(Configuration);
            coll.AddMoldsterWeb();
            coll.AddMoldsterConfigurator();
            coll.AddMoldsterServerGeneration();
            coll.AddMoldsterRazorHelpers();
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

        public override void ConfigureHttp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMoldsterServerGeneration();
            base.ConfigureHttp(app, env);
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
