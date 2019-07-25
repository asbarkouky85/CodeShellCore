
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Asga.Auth;
using Asga.Auth.Views;

namespace Asga.Security
{
    public class RoleCacheResponse
    {
        public List<RoleCacheDto> RoleCacheDtos { get; set; }
        public DateTime LastUpdate { get; set; }
        public static Expression<Func<Role, RoleCacheDto>> Expression
        {
            get
            {
                return e => new RoleCacheDto
                {
                    RoleId = e.Id,
                    ResourceActionVs = e.RoleResourceActions.Select(x => new ResourceActionV
                     {
                         Id = x.ResourceAction.Resource.Domain.Name + "__" + x.ResourceAction.Resource.Name,
                         Action = e.Name
                     }),
                    ResourceVs = e.RoleResources.Select(x => new ResourceV
                    {
                        Id = x.Resource.Domain.Name + "__" + x.Resource.Name,
                        CanInsert = x.CanInsert,
                        CanDelete = x.CanDelete,
                        CanUpdate = x.CanUpdate,
                        CanViewDetails = x.CanViewDetails
                    })

                     
                };
            }
        }


    }
}
