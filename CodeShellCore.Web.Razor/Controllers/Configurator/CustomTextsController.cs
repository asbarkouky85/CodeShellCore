using CodeShellCore.Linq;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Controllers.Configurator
{
    public class CustomTextsController : BaseApiController
    {
        private readonly ICustomTextService service;

        public CustomTextsController(ICustomTextService serv)
        {
            service = serv;
        }

        public IActionResult Get([FromBody] CustomTextRequest req, [FromQuery] LoadOptions opts)
        {
            return Respond(service.Get(req, opts));
        }

        public IActionResult SaveChanges([FromBody] IEnumerable<CustomText> lst)
        {
            return Respond(service.SaveChanges(lst));
        }
    }
}
