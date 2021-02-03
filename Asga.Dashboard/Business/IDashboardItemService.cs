using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Dashboard.Business
{
    public interface IDashboardItemService<T>
         where T : class, IDashBoardQuery
    {
        LoadResult<DashboardDTO> GetItem(string listName, T opts);
    }
}
