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
        protected override bool useLocalization => false;

        protected override CultureInfo defaultCulture => new CultureInfo("en");

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);

            coll.AddMoldsterWeb();
            coll.AddMoldsterConfigurator();
            coll.AddMoldsterServerGeneration();
            coll.AddMoldsterRazorHelpers();
        }

        public override void ConfigureHttp(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMoldsterServerGeneration();
            base.ConfigureHttp(app, env);
        }

        protected override void OnReady()
        {
            base.OnReady();
            this.ConfigureAngular2Razor();
        }
    }
}
