using CodeShellCore.Security;
using CodeShellCore.Security.Authorization;
using Asga.Auth.Views;
using Asga.Common.Services;
using Asga.Data;
using Asga.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asga.Common.Data;

namespace Asga.Auth.Data
{
    public class ResourceRepository : AsgaRepository<Resource,AuthContext>, IResourceRepository
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
                       Id = e.ResourceAction.Resource.Domain.Name + "__" + e.ResourceAction.Resource.Name,
                       Action = e.ResourceAction.Name
                   };
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
            return AsgaPermission.GetDictionary(q.ToList(), q2.ToList());
        }
    }
}
