using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Web.Controllers;
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

        public DeleteResult Delete(long prime)
        {
            return _service.Delete(prime);
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

        public bool IsUnique(PropertyUniqueDTO dto)
        {
            return _service.IsUnique(dto);
        }

        public SubmitResult<TenantEditDTO> Post(TenantDto obj)
        {
            return _service.Post(obj);
        }

        public SubmitResult<TenantEditDTO> Put(TenantDto obj)
        {
            return _service.Put(obj);
        }
    }

}
