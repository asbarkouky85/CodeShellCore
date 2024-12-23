using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CodeShellCore.Web.Razor.Domains
{
    public class DomainsController : BaseApiController, IDomainService
    {
        IDomainService _service;
        public DomainsController()
        {
        }

        public Task<DeleteResult> Delete(long id)
        {
            return _service.Delete(id);
        }

        public Task<PagedResult<DomainListDto>> Get(PagedListRequestDto options)
        {
            return _service.Get(options);
        }

        public Task<List<DomainListDto>> GetCategoriesTree()
        {
            return _service.GetCategoriesTree();
        }

        public Task<PagedResult<DomainListDto>> GetCollection(string id, PagedListRequestDto options)
        {
            return _service.GetCollection(id, options);
        }

        public Task<long> GetDomainId(string domain)
        {
            return _service.GetDomainId(domain);
        }

        public Task<Dictionary<string, IEnumerable<Named<object>>>> GetEditLookups(Dictionary<string, string> data)
        {
            return _service.GetEditLookups(data);
        }

        public Task<Dictionary<string, IEnumerable<Named<object>>>> GetListLookups(Dictionary<string, string> data)
        {
            return _service.GetListLookups(data);
        }

        public Task<DomainDto> GetSingle(long id)
        {
            return _service.GetSingle(id);
        }

        public Task<List<DomainListDto>> GetTenantTree(long id)
        {
            return _service.GetTenantTree(id);
        }

        public Task<List<DomainListDto>> GetTree()
        {
            return _service.GetTree();
        }

        public Task<Result> InstallModule(string assemblyName)
        {
            return _service.InstallModule(assemblyName);
        }

        public Task<bool> IsUnique(IsUniqueDto dto)
        {
            return _service.IsUnique(dto);
        }

        [HttpGet]
        public Task<Dictionary<long, int>> PageCategoryCounters()
        {
            return _service.PageCategoryCounters();
        }

        [HttpGet]
        public Task<Dictionary<long, int>> PageCounters(long id)
        {
            return _service.PageCounters(id);
        }

        public Task<EntitySubmitResult<DomainDto>> Post(DomainDto dto)
        {
            return _service.Post(dto);
        }

        public Task<EntitySubmitResult<DomainDto>> Put(DomainDto dto)
        {
            return _service.Put(dto);
        }

        public Task<Result> UpdateFiles(string assemblyName)
        {
            return _service.UpdateFiles(assemblyName);
        }
    }
}
