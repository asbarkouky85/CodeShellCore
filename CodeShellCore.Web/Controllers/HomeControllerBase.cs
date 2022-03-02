using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeShellCore.Types;
using Microsoft.AspNetCore.Mvc;

namespace CodeShellCore.Web.Controllers
{
    [Produces("text/html")]
    public class HomeControllerBase : Controller
    {
        public virtual IActionResult Index()
        {
            
            string ass = Shell.ProjectAssembly.GetName().Name;
            string ver = Shell.ProjectAssembly.GetVersionString();
            Response.ContentType = "text/html";
            return Content("<head><title>" + ass + "</title></head><h1>" + ass + "</h1><h2>Version : " + ver + "</h2>");
        }
    }
}