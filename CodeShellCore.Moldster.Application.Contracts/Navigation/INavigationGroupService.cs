using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Tenants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Navigation
{
    public interface INavigationGroupService : IDtoEntityService<long, NavigationGroupDto, PagedListRequestDto>
    {
        Task<SubmitResult> CheckForUnorderedNavigationPages(long navigationGroupId);
        Task<SubmitResult> Create(List<NavigationPageDto> navigationPageListDTOs);
        Task<SubmitResult> CreateNave(NavigationGroupDto navigationGroup);
        Task<SubmitResult> DeleteNavPage(long id);
        Task<PagedResult<NavigationGroupLookupDto>> GetAll(PagedListRequestDto opt);
        Task<PagedResult<NavigationPageListDTO>> GetPagesByNav(long naveId, PagedListRequestDto opts);
        Task<PagedResult<PageListDTO>> GetPageToAdd(PagedListRequestDto opt);
        Task<List<TenantDto>> GetTenant();
        Task<SubmitResult> SetApplyOrder(ApplyOrderDTO dto);
    }
}