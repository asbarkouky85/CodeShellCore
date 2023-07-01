using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Web.Razor.Services;
using CodeShellCore.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices.Webpack;
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
            var hot = getConfig("UseHotUpdate")?.Value == "True";

            base.ConfigureHttp(app, env);

            var hot = getConfig("UseHotUpdate")?.Value == "True";
            if (hot)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,

                });
#pragma warning restore CS0618 // Type or member is obsolete
            }

            base.ConfigureHttp(app, env);
        }
    }
}
