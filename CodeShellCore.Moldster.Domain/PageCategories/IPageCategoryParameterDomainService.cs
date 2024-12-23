using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.PageCategories
{
    public interface IPageCategoryParameterDomainService : IServiceBase
    {
        Task<SubmitResult> UpdateParameters(PageCategory p, List<PageCategoryParameter> parameters);
    }
}
