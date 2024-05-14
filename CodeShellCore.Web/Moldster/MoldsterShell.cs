using CodeShellCore.Web.Razor.Services;
using CodeShellCore.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Web.Moldster
{
    public abstract class MoldsterShell : WebShell
    {
        protected virtual bool UseLegacy => false;
        protected override bool UseCors => true;
        protected override bool IsSpa => true;
        public MoldsterShell(IConfiguration config) : base(config)
        {

        }

        public override void RegisterServices(IServiceCollection services)
        {
            base.RegisterServices(services);
            if (UseLegacy)
            {

                services.AddTransient<ISpaFallbackHandler, LegacySpaFallbackHandler>();
            }
            services.AddRazorPages();
            services.AddTransient<IRazorRenderingService, RazorRenderingService>();
        }

        public override void ConfigureHttp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            base.ConfigureHttp(app, env);
        }
    }
}
