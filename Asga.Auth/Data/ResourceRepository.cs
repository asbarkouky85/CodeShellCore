using CodeShellCore.Security;
using CodeShellCore.Security.Authorization;
using Asga.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Asga.Common.Data;

namespace Asga.Auth.Data
{
    public class ResourceRepository : AsgaRepository<Resource, AuthContext>, IResourceRepository
    {

        public ResourceRepository(AuthContext con, AsgaCollectionService ser) : base(con, ser)
        {
        }

        private IQueryable<ResourceV> GetUserResources_Query(long id)
        {
            return from e in DbContext.RoleResources
                   where e.Role.UserRoles.Select(d => d.UserId).Contains(id)
                   select new ResourceV
                   {
                       Id = e.Resource.Name,
                       CanInsert = e.CanInsert,
                       CanDelete = e.CanDelete,
                       CanUpdate = e.CanUpdate,
                       CanViewDetails = e.CanViewDetails
                   };
        }

        private IQueryable<ResourceActionV> GetUserResourceActions_Query(long id)
        {
            return from e in DbContext.RoleResourceActions
                   where e.Role.UserRoles.Select(d => d.UserId).Contains(id)
                   select new ResourceActionV
                   {
                       Id = e.ResourceAction.Resource.Name,
                       Action = e.ResourceAction.Name
                   };
        }

        public List<ResourceActionV> GetRoleResourceActions(object roleId)
        {
            var q = from e in DbContext.RoleResourceActions
                    where e.RoleId.Equals(roleId)
                    select new ResourceActionV
                    {
                        Id = e.ResourceAction.Resource.Name,
                        Action = e.ResourceAction.Name
                    };
            return q.ToList();
        }

        public List<ResourceV> GetRoleResources(object roleId)
        {
            var q = from e in DbContext.RoleResources
                    where e.RoleId.Equals(roleId)
                    select new ResourceV
                    {
                        Id = e.Resource.Name,
                        CanInsert = e.CanInsert,
                        CanDelete = e.CanDelete,
                        CanUpdate = e.CanUpdate,
                        CanViewDetails = e.CanViewDetails
                    };
            return q.ToList();
        }

        public Dictionary<string, Permission> GetUserPermissions(object c)
        {

            long id = 0;
            if (c is string)
                long.TryParse(c as string, out id);
            else
                id = (long)c;
            var q = GetUserResources_Query(id);
            var q2 = GetUserResourceActions_Query(id);
            return AccessibilityPermissions.GetDictionary(q.ToList(), q2.ToList());
        }
    }
}
