using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeShellCore.Web.Razor.Moldster.Controllers
{
    public class TenantsController : BaseApiController, ITenantService
    {
        ITenantService _service;

        public TenantsController(ITenantService configTenantService)
        {
            _service = configTenantService;
        }

        [HttpDelete]
        public DeleteResult Delete(long id)
        {
            return _service.Delete(id);
        }

        
        public LoadResult<TenantDto> Get(LoadOptions opts)
        {
            return _service.Get(opts);
        }

        public LoadResult<TenantDto> GetCollection(string id, LoadOptions opts)
        {
            return _service.GetCollection(id, opts);
        }

        public Dictionary<string, IEnumerable<Named<object>>> GetEditLookups(Dictionary<string, string> data)
        {
            return _service.GetEditLookups(data);
        }

        public Dictionary<string, IEnumerable<Named<object>>> GetListLookups(Dictionary<string, string> data)
        {
            return _service.GetListLookups(data);
        }


        public TenantEditDTO GetSingle(long id)
        {
            return _service.GetSingle(id);
        }

        [HttpGet]
        public bool IsUnique(IsUniqueDto dto)
        {
            return _service.IsUnique(dto);
        }

        [HttpPost]
        public SubmitResult<TenantEditDTO> Post([FromBody]TenantDto obj)
        {
            return _service.Post(obj);
        }

        [HttpPut]
        public SubmitResult<TenantEditDTO> Put([FromBody]TenantDto obj)
        {
            return _service.Put(obj);
        }
    }

}
