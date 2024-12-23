using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.Tenants
{
    public class TenantsController : BaseApiController, ITenantService
    {
        ITenantService _service;

        public TenantsController(ITenantService configTenantService)
        {
            _service = configTenantService;
        }

        public Task<DeleteResult> Delete(long id)
        {
            return _service.Delete(id);
        }

        public Task<PagedResult<TenantDto>> Get(PagedListRequestDto options)
        {
            return _service.Get(options);
        }

        public Task<PagedResult<TenantDto>> GetCollection(string id, PagedListRequestDto options)
        {
            return _service.GetCollection(id, options);
        }

        public Task<Dictionary<string, IEnumerable<Named<object>>>> GetEditLookups(Dictionary<string, string> data)
        {
            return _service.GetEditLookups(data);
        }

        public Task<Dictionary<string, IEnumerable<Named<object>>>> GetListLookups(Dictionary<string, string> data)
        {
            return _service.GetListLookups(data);
        }

        public Task<TenantEditDTO> GetSingle(long id)
        {
            return _service.GetSingle(id);
        }

        public Task<bool> IsUnique(IsUniqueDto dto)
        {
            return _service.IsUnique(dto);
        }

        public Task<EntitySubmitResult<TenantEditDTO>> Post(TenantDto dto)
        {
            return _service.Post(dto);
        }

        public Task<EntitySubmitResult<TenantEditDTO>> Put(TenantDto dto)
        {
            return _service.Put(dto);
        }
    }

}
