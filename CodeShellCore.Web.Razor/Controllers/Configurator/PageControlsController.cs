using CodeShellCore.Linq;
using CodeShellCore.Moldster.Configurator.Services;
using CodeShellCore.Moldster;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;


namespace CodeShellCore.Web.Razor.Configurator
{
    public class PageControlsController : EntityController<PageControl, long>
    {
        PageControlService _service;
        PagesService pages => GetService<PagesService>();
        ConfiguratorLookupService lookups => GetService<ConfiguratorLookupService>();
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
