﻿using CodeShellCore.Linq;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster;

namespace CodeShellCore.Web.Razor.Moldster.Controllers
{
    public class PageControlsController : EntityController<PageControl, long>
    {
        PageControlService _service;
        PagesDataService pages => GetService<PagesDataService>();
        MoldsterLookupService lookups => GetService<MoldsterLookupService>();
        public PageControlsController(PageControlService service) : base(service)
        {
            _service = service;
        }

        public IActionResult GetControlByPageId([FromQuery] LoadOptions opt)
        {
            return Respond(_service.GetControlByPageId(opt));
        }

        public IActionResult GetEditLookups([FromQuery] Dictionary<string, string> data)
        {
            return Respond(lookups.PageControlList(data));
        }
    }
}
