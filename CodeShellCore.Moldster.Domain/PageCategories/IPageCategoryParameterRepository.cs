using CodeShellCore.Data;
using CodeShellCore.Moldster.Pages.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.PageCategories
{
    public interface IPageCategoryParameterRepository : IRepository<PageCategoryParameter>
    {
        Task UpdateParameters(long id, List<PageCategoryParameter> parameters);
        Task<IEnumerable<PageCategoryParameterWithPageId>> FindForPageParameterUpdate(long id, long tenantId);
    }
}
