using CodeShellCore.Data;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Navigation
{
    public interface INavigationPageRepository : IRepository<NavigationPage>
    {
        LoadResult<T> GetUnderNave<T>(long navId, LoadOptions opt) where T : class;
        void SetDisplayOrder(long naveId);
    }
}
