using Asga.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Linq;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Data.ConfiguredCollections;
using Asga.Security;
using CodeShellCore.Helpers;
using CodeShellCore.Web;
using CodeShellCore.FileServer;
using CodeShellCore.FileServer.Data;
using Asga.Public;
using Asga.Auth.Data;
using CodeShellCore.DependencyInjection;

namespace Example.UI
{
    public class UIShell : WebShell
    {
        protected override bool useLocalization => true;
        protected override string localizationAssembly => "Example";
        protected override bool IsSpa => true;
        protected override bool UseCors => true;

        protected override CultureInfo defaultCulture => new CultureInfo("en");
        public UIShell(IConfiguration config) : base(config)
        {
        }

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            coll.AddAsgaAuthModule(Configuration, false);
            coll.AddTokenSecurity<AuthUnit>();
            coll.AddPublicModule(Configuration, false);
            coll.AddFileServerModule(Configuration);
            
        }

        protected override void OnApplicationStarted(IServiceProvider prov)
        {
            base.OnApplicationStarted(prov);
            prov.MigrateAuthDb();
            prov.MigrateContext<FileServerDbContext>();
            prov.MigrateContext<AsgaPublicContext>();
        }

        protected override void OnReady()
        {
            base.OnReady();
            RootInjector.GetService<ICollectionConfigService>().RegisterCollection<User>("MaleUsers", u =>
            {
                var rols = u.UserAs<UserDTO>().Roles.Select(e => long.Parse(e)).ToList();
                return d => d.UserRoles.Any(e => rols.Any(f => e.RoleId == f));
            });
            //var tok = Utils.CreateClientToken(new AppClient { ClientId = "Test.App" });
            //Console.WriteLine(tok);
        }
    }
}
