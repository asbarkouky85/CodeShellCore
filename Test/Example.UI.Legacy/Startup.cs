using CodeShellCore.Web;
using Microsoft.Extensions.Configuration;

namespace Example.UI.Legacy
{
    public class Startup : ShellStartup<UIShell>
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
