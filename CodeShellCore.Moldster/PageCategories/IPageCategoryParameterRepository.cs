using CodeShellCore.Data;
using CodeShellCore.Moldster.PageCategories.Dtos;
using CodeShellCore.Moldster.Pages.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.PageCategories
{
    public interface IPageCategoryParameterRepository : IRepository<PageCategoryParameter>
    {
        void UpdateParameters(long id, List<PageCategoryParameterDTO> parameters);
        IEnumerable<PageCategoryParameterWithPageId> FindForPageParameterUpdate(long id, long tenantId);
    }
}
