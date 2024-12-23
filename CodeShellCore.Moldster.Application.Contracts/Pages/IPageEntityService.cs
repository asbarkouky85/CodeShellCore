using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Sql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageEntityService : IDtoEntityService<long, PagedListRequestDto, PageListDTO, CreatePageDTO, CreatePageDTO, CreatePageDTO>
    {
        Task<SubmitResult> SetViewParams(ViewParamsSetter @params);
        Task<PagedResult<PageListDTO>> GetPagesByDomain(long domainId, PagedListRequestDto opt);
        Task<IEnumerable<PageParameterEditDto>> GetViewParameters(long id);
        Task<PageCustomizationDTO> GetCustomizationData(long id);
        Task<SubmitResult> ApplyCustomization(PageCustomizationDTO dto);
        Task<PagedResult<PageListDTO>> FindPages(PagedListRequestDto opts, FindPageRequest request);
    }
}
