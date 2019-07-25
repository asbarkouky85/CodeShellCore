using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Data;

namespace Asga.Auth.Views
{
    public class ResourceV
    {
        public string Id { get; set; }
        public bool CanInsert { get; set; }
        public bool CanDelete { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanViewDetails { get; set; }

        public static System.Linq.Expressions.Expression<Func<RoleResource, ResourceV>> Expression
        {
            get
            {
                return e => new ResourceV
                {
                    Id = e.Resource.Domain.Name + "__" + e.Resource.Name,
                    CanInsert = e.CanInsert,
                    CanDelete = e.CanDelete,
                    CanUpdate = e.CanUpdate,
                    CanViewDetails = e.CanViewDetails
                };
            }
        }
    }
}
