using CodeShellCore.Data.Lookups;
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

        public IEnumerable<Named<object>> FindAsLookup(string collectionId = null)
        {
            throw new NotImplementedException();
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

        public IEnumerable<Named<object>> FindAsLookup(string collectionId = null)
        {
            throw new NotImplementedException();
        }

        public string[] GetResourcesWithCollections()
        {
            throw new NotImplementedException();
        }

        public List<ResourceActionV> GetRoleResourceActions(object roleId)
        {
            return new List<ResourceActionV>();
        }

        public List<ResourceV> GetRoleResources(object roleId)
        {
            return new List<ResourceV>();
        }
    }
}
