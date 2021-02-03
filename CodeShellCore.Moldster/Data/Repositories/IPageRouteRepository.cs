using CodeShellCore.Data;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Data.Repositories
{
    public interface IPageRouteRepository : IRepository<PageRoute>
    {
        IEnumerable<PageRouteDTO> FindForJson(long tenantId, long? categoryId = null);
        PageRouteDTO FindByPage(long id);
    }
}
