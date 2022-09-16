using CodeShellCore.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.UI.Controllers
{
    public class HomeController : SpaHomeController
    {
        public override string GetDefaultTitle(string loc)
        {
            return "Example";
        }
    }
}
