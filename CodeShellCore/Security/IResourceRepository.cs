using CodeShellCore.Data;
using CodeShellCore.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security
{
    public interface IResourceRepository : IRepository
    {
        List<ResourceActionV> GetRoleResourceActions(object roleId);
        List<ResourceV> GetRoleResources(object roleId);
    }
}
