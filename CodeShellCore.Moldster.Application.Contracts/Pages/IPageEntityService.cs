using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageEntityService : IDtoEntityService<long, PagedListRequestDto, PageListDTO, CreatePageDTO, CreatePageDTO, CreatePageDTO>
    {
        SubmitResult SetViewParams(ViewParamsSetter @params);
        PagedResult<PageListDTO> GetPagesByDomain(long domainId, PagedListRequestDto opt);
        IEnumerable<PageParameterEditDto> GetViewParameters(long id);
        PageCustomizationDTO GetCustomizationData(long id);
        SubmitResult ApplyCustomization(PageCustomizationDTO dto);
        PagedResult<PageListDTO> FindPages(PagedListRequestDto opts, FindPageRequest request);
    }
}
