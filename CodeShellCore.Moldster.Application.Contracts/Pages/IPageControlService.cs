using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageControlService : IDtoEntityService<long, PageControlDto, PagedListRequestDto>
    {
        Task<PagedResult<PageControlListDTO>> GetControlByPageId(PagedListRequestDto opt);
        Task<SubmitResult> UpdatePageControls(List<PageControlListDTO> pageControls);
    }
}