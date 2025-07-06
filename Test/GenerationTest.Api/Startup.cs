using CodeShellCore.Web;
using Microsoft.Extensions.Configuration;

namespace GenerationTest.Api
{
    public class Startup : WebModuleStartup<GenerationTestApiModule>
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
