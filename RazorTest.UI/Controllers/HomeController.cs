using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorTest.UI.Controllers
{
    public class HomeController : CodeShellCore.Web.Controllers.BaseMvcController

    {

        public ActionResult Index()
        {
            return View("~/Views/Test.cshtml");
        }
    }
}
