using CodeShellCore.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;

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
    }
}
