using CodeShellCore.Web;
using Microsoft.Extensions.Configuration;

namespace ExampleProject.Config.Api
{
    public class Startup : ShellStartup<ConfigShell>
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
