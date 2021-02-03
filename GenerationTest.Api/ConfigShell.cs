using System.Globalization;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Web.Razor;
using CodeShellCore.Moldster;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using GenerationTest.Api.Data;

namespace GenerationTest.Api
{
    public class ConfigShell : CodeShellCore.Web.WebShell
    {
        protected override bool useLocalization { get { return false; } }

        protected override CultureInfo defaultCulture => new CultureInfo("en");

        public ConfigShell(IConfiguration config) : base(config)
        {
        }
        string enviro;
        public override void ConfigureHttp(IApplicationBuilder app, IHostingEnvironment env)
        {
            enviro = env.EnvironmentName;
            base.ConfigureHttp(app, env);
        }
        bool testing = false;
        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            coll.AddMoldsterWeb();
            coll.AddMoldsterConfigurator();
            coll.AddCodeShellEmbeddedViews();
            coll.AddMoldsterRazorHelpers();
            var d = getConfig("ConnectionStrings:Moldster");
            if (d != null)
                testing = d.Value == "TEST";

            if (testing)
            {
                coll.AddTestConfigDB();
            }

        }

        protected override void OnReady()
        {
            base.OnReady();
            this.ConfigureAngular2Razor();
            if (testing)
                TestConfigData.Initialize();
        }
    }
}
