using CodeShellCore.Data.Services;
using CodeShellCore.Moldster.Configurator.Services;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Configurator
{
    public class DomainsController : EntityController<Domain, long>
    {
        DomainService _service;
        public DomainsController(DomainService configDomainService) : base(configDomainService)
        {
            _service = configDomainService;
        }

        public IActionResult GetTree()
        {
            return Respond(_service.GetTree());
        }

        public IActionResult PageCategoryCounters()
        {
            Dictionary<long, int> dic = _service.PageCategoryCounters();
            return Respond(dic);
        }

        public IActionResult PageCounters(long id)
        {
            Dictionary<long, int> dic = _service.PageCounters(id);
            return Respond(dic);
        }
    }
}
