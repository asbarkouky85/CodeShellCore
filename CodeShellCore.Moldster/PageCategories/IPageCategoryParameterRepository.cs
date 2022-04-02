using CodeShellCore.Data;
using CodeShellCore.Moldster.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.PageCategories
{
    public interface IPageCategoryParameterRepository : IRepository<PageCategoryParameter>
    {
        void UpdateParameters(long id, List<PageCategoryParameter> parameters);
        IEnumerable<PageCategoryParameterWithPageId> FindForPageParameterUpdate(long id, long tenantId);
    }
}
