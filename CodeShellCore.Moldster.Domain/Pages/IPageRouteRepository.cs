using CodeShellCore.Data;
using CodeShellCore.Moldster.Pages.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageRouteRepository : IRepository<PageRoute>
    {
        Task<IEnumerable<PageRouteView>> FindForJson(long tenantId, long? categoryId = null);
        Task<PageRouteView> FindByPage(long id);
    }
}
