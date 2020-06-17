using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Web;
using CodeShellCore.Web.Moldster;
using Asga.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Asga.Web;
using CodeShellCore.Security.Authorization;

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

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            coll.AddAuthModule(false);
            coll.AddAsgaWeb();

            coll.AddTransient<IAuthorizationService, AuthenticatedOnlyAuthorizationService>();
        }
    }
}
