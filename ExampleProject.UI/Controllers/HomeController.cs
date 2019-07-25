using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using CodeShellCore.Web.Moldster;

using ExampleProject.UI.Models;

namespace ExampleProject.UI.Controllers
{
    [HtmlExceptionFilter]
    public class HomeController : MoldsterUIController
    {
        public HomeController()
        {
        }

        private ServerConfig _config = new ServerConfig();
        public override string DefaultDomain => "MainApp";
        public override string[] Domains => new string[] { "MainApp" };
        public override ServerConfigBase ServerConfig => _config;

        public override string GetDefaultTitle(string loc)
        {
            return "MainApp";
        }


    }
}
