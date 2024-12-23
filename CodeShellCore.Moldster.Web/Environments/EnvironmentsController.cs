using CodeShellCore.Linq;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.Environments.Services;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.Environments
{
    [ApiAuthorize(AllowAll = true)]
    public class EnvironmentsController : BaseApiController, IEnvironmentsService
    {
        private readonly IEnvironmentsService service;

        public EnvironmentsController(IEnvironmentsService service)
        {
            this.service = service;
        }

        public Task Delete(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedResult<MoldsterEnvironment>> Get()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<string>> GetDatabaseList(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<MoldsterEnvironment> Post(MoldsterEnvironment dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<MoldsterEnvironment> Put(MoldsterEnvironment env)
        {
            throw new System.NotImplementedException();
        }
    }
}
