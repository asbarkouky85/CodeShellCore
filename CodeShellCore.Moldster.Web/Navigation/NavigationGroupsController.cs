using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Web.Controllers;

namespace CodeShellCore.Web.Razor.Navigation
{
    public class NavigationGroupsController : BaseApiController, INavigationGroupService
    {
        INavigationGroupService _service;
        public NavigationGroupsController(INavigationGroupService service)
        {
            _service = service;
        }

        public Task<SubmitResult> CheckForUnorderedNavigationPages(long navigationGroupId)
        {
            return _service.CheckForUnorderedNavigationPages(navigationGroupId);
        }

        public Task<SubmitResult> Create(List<NavigationPageDto> navigationPageListDTOs)
        {
            return _service.Create(navigationPageListDTOs);
        }

        public Task<SubmitResult> CreateNave(NavigationGroupDto navigationGroup)
        {
            return _service.CreateNave(navigationGroup);
        }

        public Task<DeleteResult> Delete(long id)
        {
            return _service.Delete(id);
        }

        public Task<SubmitResult> DeleteNavPage(long id)
        {
            return _service.DeleteNavPage(id);
        }

        public Task<PagedResult<NavigationGroupDto>> Get(PagedListRequestDto options)
        {
            return _service.Get(options);
        }

        public Task<PagedResult<NavigationGroupLookupDto>> GetAll(PagedListRequestDto opt)
        {
            return _service.GetAll(opt);
        }

        public Task<PagedResult<NavigationGroupDto>> GetCollection(string id, PagedListRequestDto options)
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

        public Task<PagedResult<NavigationPageListDTO>> GetPagesByNav(long naveId, PagedListRequestDto opts)
        {
            return _service.GetPagesByNav(naveId, opts);
        }

        public Task<PagedResult<PageListDTO>> GetPageToAdd(PagedListRequestDto opt)
        {
            return _service.GetPageToAdd(opt);
        }

        public Task<NavigationGroupDto> GetSingle(long id)
        {
            return _service.GetSingle(id);
        }

        public Task<List<TenantDto>> GetTenant()
        {
            return _service.GetTenant();
        }

        public Task<bool> IsUnique(IsUniqueDto dto)
        {
            return _service.IsUnique(dto);
        }

        public Task<EntitySubmitResult<NavigationGroupDto>> Post(NavigationGroupDto dto)
        {
            return _service.Post(dto);
        }

        public Task<EntitySubmitResult<NavigationGroupDto>> Put(NavigationGroupDto dto)
        {
            return _service.Put(dto);
        }

        public Task<SubmitResult> SetApplyOrder(ApplyOrderDTO dto)
        {
            return _service.SetApplyOrder(dto);
        }
    }
}
