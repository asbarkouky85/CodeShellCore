using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using CodeShellCore.Web.Moldster;

namespace ExampleProject.UI.Controllers
{
    [HtmlExceptionFilter]
    public class HomeController : MoldsterUIController
    {
        public HomeController()
        {
        }

        public override string DefaultDomain => "MainApp";
        public override string[] Domains => new string[] { "MainApp" };

        public override string GetDefaultTitle(string loc)
        {
            return "MainApp";
        }


    }
}
