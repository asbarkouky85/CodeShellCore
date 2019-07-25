using System;
using System.Linq.Expressions;

namespace Asga.Auth.Views
{
    public class ResourceActionV
    {
        public string Id { get; set; }
        public string Action { get; set; }

        public static Expression<Func<RoleResourceAction, ResourceActionV>> Expression
        {
            get
            {
                return e => new ResourceActionV
                {
                    Id = e.ResourceAction.Resource.Domain.Name + "__" + e.ResourceAction.Resource.Name,
                    Action = e.ResourceAction.Name
                };
            }
        }
    }
}
