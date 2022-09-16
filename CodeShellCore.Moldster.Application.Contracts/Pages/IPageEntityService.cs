using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageEntityService : IDtoEntityService<long, LoadOptions, PageListDTO, CreatePageDTO, CreatePageDTO, CreatePageDTO>
    {
        SubmitResult SetViewParams(ViewParamsSetter @params);
        LoadResult<PageListDTO> GetPagesByDomain(long domainId, LoadOptions opt);
        IEnumerable<PageParameterEditDto> GetViewParameters(long id);
        PageCustomizationDTO GetCustomizationData(long id);
        SubmitResult ApplyCustomization(PageCustomizationDTO dto);
        LoadResult<PageListDTO> FindPages(LoadOptions opts, FindPageRequest request);
    }
}
