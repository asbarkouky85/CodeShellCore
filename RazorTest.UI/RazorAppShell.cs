using CodeShellCore.Web;
using CodeShellCore.Web.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RazorTest.UI
{
    public class RazorAppShell : WebShell
    {
        public RazorAppShell(IConfiguration config) : base(config)
        {
        }

        protected override bool useLocalization => true;

        protected override CultureInfo defaultCulture => new CultureInfo("ar");

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            coll.AddMvcRazorHelpers();
            coll.AddCodeShellEmbeddedViews();
        }

        protected override void OnReady()
        {
            base.OnReady();
            this.ConfigureMvcRazor();
        }
    }
}
