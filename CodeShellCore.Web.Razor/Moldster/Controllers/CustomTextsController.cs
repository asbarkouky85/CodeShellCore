﻿using CodeShellCore.Linq;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Localization.Services;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeShellCore.Web.Razor.Moldster.Controllers
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