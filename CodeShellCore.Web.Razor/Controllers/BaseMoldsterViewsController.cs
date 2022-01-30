using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Definitions;

using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using CodeShellCore.Web.Razor;
using CodeShellCore.Web.Razor.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Controllers
{
    [ApiExceptionFilter]
    public class BaseMoldsterViewsController : BaseController
    {
        private readonly ServerViewsService views;

        public BaseMoldsterViewsController(ServerViewsService views)
        {
            this.views = views;
        }

        public virtual IActionResult GetPage([FromQuery]PageAcquisitorDTO dto)
        {
            var html = views.GetPage(dto);
            return Content(html);
        }

        public virtual IActionResult GetPageById(long id)
        {
            var html = views.GetPageById(id);
            return Content(html);
        }

        public virtual IActionResult GetMainComponent([FromQuery]PageAcquisitorDTO dto)
        {
            var html = views.GetMainComponent(dto.ViewPath);
            return Content(html);
        }
    }
}
