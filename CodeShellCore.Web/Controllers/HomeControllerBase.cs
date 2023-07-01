using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeShellCore.Types;
using Microsoft.AspNetCore.Mvc;

namespace CodeShellCore.Web.Controllers
{
    [Produces("text/html")]
    public abstract class HomeControllerBase : Controller
    {
        public virtual IActionResult Index()
        {
            Response.ContentType = "text/html";
            return Content(WebUtils.GetAssemblyInfoHtml());
        }
    }
}