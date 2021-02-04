using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;

namespace CodeShellCore.Web.Moldster
{
    public abstract class MoldsterShell : WebShell
    {
        public MoldsterShell(IConfiguration config) : base(config)
        {

        }

        public override void ConfigureHttp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            base.ConfigureHttp(app, env);
        }

        public override void RegisterRoutes(IRouteBuilder routes)
        {
            base.RegisterRoutes(routes);
            routes.MapRoute(
                name: "domain",
                template: "{id}",
                defaults: new { controller = "Home", action = "Index" });

            routes.MapRoute(
                name: "ds",
                template: "SetLocale/{lang}",
                defaults: new { controller = "Home", action = "SetLocale" });


            routes.MapSpaFallbackRoute(
                name: "spa-fallback",
                defaults: new { controller = "Home", action = "Index" });
        }
    }
}
