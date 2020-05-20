using CodeShellCore.Data;
using CodeShellCore.Moldster.Db.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data
{
    public interface IPageCategoryParameterRepository : IRepository<PageCategoryParameter>
    {
        void UpdateParameters(long id, List<PageCategoryParameterDTO> parameters);
        IEnumerable<PageCategoryParameterWithPageId> FindForPageParameterUpdate(long id, long tenantId);
    }
}
