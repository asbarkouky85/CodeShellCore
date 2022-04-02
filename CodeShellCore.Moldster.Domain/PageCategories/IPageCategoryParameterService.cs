using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.PageCategories
{
    public interface IPageCategoryParameterService : IServiceBase
    {
        SubmitResult UpdateParameters(PageCategory p, List<PageCategoryParameter> parameters);
    }
}
