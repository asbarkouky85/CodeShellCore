using System.Globalization;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

using CodeShellCore.Web;

using CodeShellCore.Security.Authentication;
using CodeShellCore.Web.Security;
using CodeShellCore.Data;

namespace ExampleProject.UI
{
    public class UIShell : WebShell
    {
        public UIShell(IConfiguration config) : base(config)
        {
        }

        protected override bool useLocalization => false;

        protected override CultureInfo defaultCulture => new CultureInfo("ar");

        public override void ConfigureHttp(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();


           
            var hot = getConfig("UseHotUpdate")?.Value == "True";
            if (hot)
            {
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            base.ConfigureHttp(app, env);
        }
        
        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
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
