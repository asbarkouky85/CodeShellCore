
using Microsoft.Extensions.Configuration;
using CodeShellCore.Web.Razor;

namespace Example.Config.Api
{
    public class ConfigShell : MoldsterWebShell
    {
        public ConfigShell(IConfiguration config) : base(config)
        {
        }
    }
}
