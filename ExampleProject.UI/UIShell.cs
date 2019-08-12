using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

using CodeShellCore.Web;
using CodeShellCore.Text;

using Asga.Auth;
using Asga.Web;
using Microsoft.EntityFrameworkCore;
using CodeShellCore;
using System.Collections.Generic;

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
            coll.AddAuthModule();
            coll.AddAsgaWeb();

            coll.AddDbContext<AuthContext>(d => d.UseInMemoryDatabase("auth"));
        }

        protected override void OnReady()
        {
            using (var sc = Shell.GetScope())
            {
                var c = sc.ServiceProvider.GetService<AuthContext>();
                c.Resources.Add(new Resource
                {
                    Id = 1,
                    Name = "Users"
                });

                c.Resources.Add(new Resource
                {
                    Id = 2,
                    Name = "Roles"
                });

                c.Roles.Add(new Role
                {
                    Id = 1,
                    Name = "Admin",
                    RoleResources = new List<RoleResource>
                {
                    new RoleResource { Id=1,ResourceId=1,CanViewDetails=true,CanDelete=true,CanInsert=true },
                    new RoleResource { Id=2,ResourceId=2,CanViewDetails=true,CanDelete=false }
                }
                });
                c.Users.Add(new User
                {
                    Id = 1,
                    LogonName = "admin",
                    Password = "12345".ToMD5(),
                    UserRoles = new List<UserRole> {
                    new UserRole{ Id=1,RoleId=1}
                }
                });
                c.SaveChanges();
            }
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
