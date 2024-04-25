using CodeShellCore.Data;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Navigation
{
    public interface INavigationPageRepository : IRepository<NavigationPage>
    {
        PagedResult<T> GetUnderNave<T>(long navId, PagedListRequest opt) where T : class;
        void SetDisplayOrder(long naveId);
    }
}
