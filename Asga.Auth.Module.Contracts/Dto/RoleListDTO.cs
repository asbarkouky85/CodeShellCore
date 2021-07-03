using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Asga.Auth.Dto
{
    public class RoleListDTO : BaseDTO<long>, IDTO<Role>
    {
        public string Name { get; set; }

        public static Expression<Func<Role, RoleListDTO>> Expression =>
            d => new RoleListDTO
            {
                Id = d.Id,
                Name = d.Name,
                CreatedBy = d.CreatedBy,
                CreatedOn = d.CreatedOn
            };
    }
}
