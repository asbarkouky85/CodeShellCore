using CodeShellCore.Data;
using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Moldster.Navigation.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Data.Repositories
{
    public interface INavigationGroupRepository : IRepository<NavigationGroup>
    {
        NavigationGroup GetNavigationGroup(string name);
        IEnumerable<NavigationGroupDTO> GetTenantNavs(long modId);
    }
}
