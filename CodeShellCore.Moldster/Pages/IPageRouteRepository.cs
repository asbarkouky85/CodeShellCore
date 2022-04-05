using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageRouteRepository : IRepository<PageRoute>
    {
        IEnumerable<PageRouteDTO> FindForJson(long tenantId, long? categoryId = null);
        PageRouteDTO FindByPage(long id);
    }
}
