
using Microsoft.Extensions.Configuration;
using CodeShellCore.Web.Razor;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Moldster;

namespace Example.Config.Api
{
    public class ConfigShell : MoldsterWebShell
    {
        public ConfigShell(IConfiguration config) : base(config)
        {
        }

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            coll.AddMoldsterModules(e =>
            {
                e.Register("Asga.Auth.Molds", "{PARENT}/Asga.Auth.Molds");
                e.Register("Asga.Public.Molds", "{PARENT}/Asga.Public.Molds");
            });
        }
    }
}
