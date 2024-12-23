using CodeShellCore.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Navigation
{
    public interface INavigationGroupRepository : IRepository<NavigationGroup>
    {
        Task<NavigationGroup> GetNavigationGroup(string name);
        Task<IEnumerable<T>> GetTenantNavs<T>(long tenantId);
    }
}
