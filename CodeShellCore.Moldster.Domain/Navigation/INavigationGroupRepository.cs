using CodeShellCore.Data;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Navigation
{
    public interface INavigationGroupRepository : IRepository<NavigationGroup>
    {
        NavigationGroup GetNavigationGroup(string name);
        IEnumerable<T> GetTenantNavs<T>(long tenantId);
    }
}
