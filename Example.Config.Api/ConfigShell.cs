using System.Globalization;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Web.Razor;
using CodeShellCore.Moldster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

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

            coll.AddMoldsterModules(d =>
            {
                d.Register("Asga.Auth.Molds", "{PARENT}/Asga.Auth.Molds");
                d.Register("Asga.Public.Molds", "{PARENT}/Asga.Public.Molds");
            });
        }
    }
}
