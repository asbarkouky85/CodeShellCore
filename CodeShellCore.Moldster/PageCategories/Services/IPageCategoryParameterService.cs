using CodeShellCore.Data.Helpers;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.PageCategories.Services
{
    public interface IPageCategoryParameterService : IServiceBase
    {
        SubmitResult UpdateParameters(PageCategory p, List<PageCategoryParameterDTO> parameters);
    }
}
