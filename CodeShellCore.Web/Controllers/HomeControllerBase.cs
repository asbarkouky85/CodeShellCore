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
        protected virtual bool UseSwagger => false;
        public virtual IActionResult Index()
        {
            if (UseSwagger)
            {
                return Redirect("/swagger");
            }
            else
            {
                Response.ContentType = "text/html";
                return Content(WebUtils.GetAssemblyInfoHtml());
            }
            
        }
    }
}