using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Resources;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeShellCore.Web.Razor.Moldster.Controllers
{
    public class ResourcesController : EntityController<Resource, long>, IEntityController<Resource, long>, ILookupLoaderController
    {
        private readonly IEntityService<Resource> service;
        private readonly IConfigUnit unit;

        public ResourcesController(IEntityService<Resource> service, IConfigUnit unit) : base(service)
        {
            this.service = service;
            this.unit = unit;
        }

        public override IActionResult Get([FromQuery] LoadOptions opt)
        {
            var res = unit.ResourceRepository.FindAndMap(opt.GetOptionsFor<ResourceListDTO>());
            return Respond(res);
        }

        public IActionResult GetEditLookups([FromQuery] Dictionary<string, string> data)
        {
            object ob = GetService<MoldsterLookupService>().ResourceEdit(data);
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
