﻿using System.Globalization;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Web.Razor;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Web.Razor.Angular;

namespace ExampleProject.Config.Api
{
    public class ConfigShell : CodeShellCore.Web.WebShell
    {
        protected override bool useLocalization { get { return false; } }

        protected override CultureInfo defaultCulture => new CultureInfo("en");

        public ConfigShell(IConfiguration config) : base(config)
        {
        }

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            coll.AddMoldsterWeb(MoldsType.Db);
            coll.AddCodeShellEmbeddedViews();
            coll.AddAngular2Helpers();
        }

        protected override void OnReady()
        {
            base.OnReady();
            this.ConfigureAngular2Razor();
        }
    }
}
