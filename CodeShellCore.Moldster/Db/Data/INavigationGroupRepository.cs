using CodeShellCore.Data;
using CodeShellCore.Moldster.Db.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data
{
    public interface INavigationGroupRepository : IRepository<NavigationGroup>
    {
        NavigationGroup GetNavigationGroup(string name);
        IEnumerable<NavigationGroupDTO> GetTenantNavs(long modId);
    }
}
