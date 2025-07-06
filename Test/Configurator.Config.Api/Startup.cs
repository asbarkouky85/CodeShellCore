using CodeShellCore.Web;
using Microsoft.Extensions.Configuration;

namespace Configurator.Config.Api
{
    public class Startup : WebModuleStartup<ConfiguratorConfigApiModule>
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
