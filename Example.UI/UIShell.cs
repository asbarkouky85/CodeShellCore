using CodeShellCore.Web.Moldster;
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

namespace Example.UI
{
    public class UIShell : MoldsterShell
    {
        protected override bool useLocalization => true;
        protected override string localizationAssembly => "Example";

        protected override CultureInfo defaultCulture => new CultureInfo("en");
        public UIShell(IConfiguration config) : base(config)
        {
        }

    }
}
