using CodeShellCore.Web.Moldster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace Example.UI.Legacy
{
    public class UIShell : MoldsterShell
    {
        protected override bool useLocalization => false;
        protected override bool UseLegacy => true;

        protected override CultureInfo defaultCulture => new CultureInfo("en");
        public UIShell(IConfiguration config) : base(config)
        {
        }

        public override void RegisterServices(IServiceCollection services)
        {
            base.RegisterServices(services);
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

    }
}
