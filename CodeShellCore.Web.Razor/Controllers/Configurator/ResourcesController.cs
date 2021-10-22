using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Moldster.Data;

namespace CodeShellCore.Web.Razor.Controllers.Configurator
{
    public class ResourcesController : EntityController<Resource, long>, IEntityController<Resource, long>, ILookupLoaderController
    {
        private readonly IEntityService<Resource> service;

        public ResourcesController(IEntityService<Resource> service) : base(service)
        {
            this.service = service;
        }

        public override IActionResult Get([FromQuery] LoadOptions opt)
        {
            return Respond(service.LoadDTO(ResourceListDTO.Expression, opt));
        }

        public IActionResult GetEditLookups([FromQuery] Dictionary<string, string> data)
        {
            object ob = GetService<ConfiguratorLookupService>().ResourceEdit(data);
            return Respond(ob);
        }

        public IActionResult GetListLookups([FromQuery] Dictionary<string, string> data)
        {
            return Respond();
        }

        public IActionResult Post([FromBody] Resource obj)
        {
            return DefaultPost(obj);
        }

        public IActionResult Put([FromBody] Resource obj)
        {
            return DefaultPut(obj);
        }
    }
}
