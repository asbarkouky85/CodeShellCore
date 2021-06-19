using System.Globalization;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Web.Razor;
using CodeShellCore.Moldster;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using GenerationTest.Api.Data;
using System;
using CodeShellCore.Helpers;
using CodeShellCore.Text.Localization;

namespace GenerationTest.Api
{
    public class ConfigShell : CodeShellCore.Web.Razor.MoldsterWebShell
    {
        protected override bool MigrateOnStartup => false;
        protected override bool useLocalization { get { return false; } }
        
        bool testing = false;
        protected override CultureInfo defaultCulture => new CultureInfo("en");

        public ConfigShell(IConfiguration config) : base(config)
        {
        }
        
        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);

            
            var d = getConfig("ConnectionStrings:Moldster");
            if (d != null)
                testing = d.Value == "TEST";

            if (testing)
            {
                coll.AddTestConfigDB();
            }

        }

        protected override void OnApplicationStarted(IServiceProvider prov)
        {
            base.OnApplicationStarted(prov);
            if (testing)
                TestConfigData.Initialize(prov);
        }

        protected override void OnReady()
        {
            base.OnReady();

        }
    }
}
