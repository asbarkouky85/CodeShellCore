using CodeShellCore.Data;
using CodeShellCore.Moldster.Pages.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageRouteRepository : IRepository<PageRoute>
    {
        IEnumerable<PageRouteView> FindForJson(long tenantId, long? categoryId = null);
        PageRouteView FindByPage(long id);
    }
}
