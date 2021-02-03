using CodeShellCore.Data;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Dto;
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
