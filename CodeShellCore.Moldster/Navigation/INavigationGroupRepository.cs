using CodeShellCore.Data;
using CodeShellCore.Moldster.Navigation.Dtos;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Navigation
{
    public interface INavigationGroupRepository : IRepository<NavigationGroup>
    {
        NavigationGroup GetNavigationGroup(string name);
        IEnumerable<NavigationGroupDTO> GetTenantNavs(long modId);
    }
}
