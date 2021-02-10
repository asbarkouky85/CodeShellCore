using CodeShellCore.Data;
using CodeShellCore.Moldster.Db.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data
{
    public interface IPageRouteRepository : IRepository<PageRoute>
    {
        IEnumerable<PageRouteDTO> FindForJson(long tenantId, long? categoryId = null);
        PageRouteDTO FindByPage(long id);
    }
}
