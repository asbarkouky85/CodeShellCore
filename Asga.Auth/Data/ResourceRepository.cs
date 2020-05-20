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

        public List<ResourceActionV> GetRoleResourceActions(object roleId)
        {
            if (roleId is string)
                roleId = long.Parse((string)roleId);
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
            if (roleId is string)
                roleId = long.Parse((string)roleId);
            var q = from e in DbContext.RoleResources
                    where e.RoleId.Equals(roleId)
                    select new ResourceV
                    {
                        Id = e.Resource.Name,
                        CanInsert = e.CanInsert,
                        CanDelete = e.CanDelete,
                        CanUpdate = e.CanUpdate,
                        CanViewDetails = e.CanViewDetails,
                        CollectionId = e.CollectionId != null ? e.Collection.Name : null
                    };
            return q.ToList();
        }

    }
}
