using CodeShellCore.Data.Services;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Domains.Services;

namespace CodeShellCore.Web.Razor.Controllers.Configurator
{
    public class DomainsController : EntityController<Domain, long>, ILookupLoaderController
    {
        DomainService _service;
        ConfiguratorLookupService _lookups => GetService<ConfiguratorLookupService>();
        IModulesService _modules => GetService<IModulesService>();
        IConfigUnit Unit => GetService<IConfigUnit>();
        public DomainsController(DomainService configDomainService) : base(configDomainService)
        {
            _service = configDomainService;
        }

        public IActionResult GetEditLookups([FromQuery] Dictionary<string, string> data)
        {
            return Respond(_lookups.Modules(data));
        }

        public IActionResult GetListLookups([FromQuery] Dictionary<string, string> data)
        {
            throw new NotImplementedException();
        }

        public IActionResult GetTree()
        {
            return Respond(_service.GetTree());
        }

        public IActionResult GetTenantTree(long id)
        {
            return Respond(_service.GetTree(id));
        }

        public IActionResult GetCategoriesTree()
        {
            return Respond(_service.GetCategoriesTree());
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

        public IActionResult UpdateFiles(string assemblyName)
        {
            var res = _modules.UpdateModuleFiles(assemblyName);
            return Respond(res);
        }

        public IActionResult InstallModule(string assemblyName)
        {
            var res = _modules.InstallModule(assemblyName);
            return Respond(res);
        }
    }
}
