using CodeShellCore.Web;
using Microsoft.Extensions.Configuration;

namespace Configurator.Config.Api
{
    public class Startup : ShellStartup<ConfigShell>
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
