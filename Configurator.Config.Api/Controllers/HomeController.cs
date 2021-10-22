using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Razor.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Configurator.Config.Api.Controllers
{
    public class HomeController : HomeControllerBase
    {
        public IActionResult Test()
        {
            try
            {

                var res = PartialView("~/Angular/Components/Label.cshtml", new LabelNgInput());

                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
