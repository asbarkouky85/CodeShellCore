using CodeShellCore.Data;
using CodeShellCore.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security
{
    public interface IResourceRepository : IRepository
    {
        Task<List<ResourceActionV>> GetRoleResourceActions(object roleId);
        Task<List<ResourceV>> GetRoleResources(object roleId);
        Task<string[]> GetResourcesWithCollections();
    }
}
