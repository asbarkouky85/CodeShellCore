using CodeShellCore.Linq;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.Environments.Services;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeShellCore.Web.Razor.Configurator
{
    [ApiAuthorize(AllowAll = true)]
    public class EnvironmentsController : BaseApiController, IEnvironmentsService
    {
        private readonly IEnvironmentsService service;

        public EnvironmentsController(IEnvironmentsService service)
        {
            this.service = service;
        }

        public void Delete(string name)
        {
            service.Delete(name);
        }

        [HttpGet]
        public LoadResult<MoldsterEnvironment> Get()
        {
            return service.Get();
        }

        public IEnumerable<string> GetDatabaseList(string name)
        {
            return service.GetDatabaseList(name);
        }

        public MoldsterEnvironment Post([FromBody]MoldsterEnvironment dto)
        {
            return service.Post(dto);
        }

        public MoldsterEnvironment Put([FromBody]MoldsterEnvironment env)
        {
            return service.Put(env);
        }
    }
}
