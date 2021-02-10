using CodeShellCore.Security;
using CodeShellCore.Security.Authorization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Asga.Auth.Data
{

    public class AdminRolesRepository : IRoleRepository
    {
        public IEnumerable All()
        {
            return new object[0];
        }

        public int Count()
        {
            return 0;
        }

        public IEnumerable<string> GetUserRoles(object userId)
        {
            return new string[] { };
        }
    }
    public class AdminResourceRepository : IResourceRepository
    {
        public IEnumerable All()
        {
            return new object[0];
        }

        public int Count()
        {
            return 0;
        }

        public List<ResourceActionV> GetRoleResourceActions(object roleId)
        {
            return new List<ResourceActionV>();
        }

        public List<ResourceV> GetRoleResources(object roleId)
        {
            return new List<ResourceV>();
        }

        public Dictionary<string, Permission> GetUserPermissions(object userId)
        {
            return new Dictionary<string, Permission>();
        }
    }
}
