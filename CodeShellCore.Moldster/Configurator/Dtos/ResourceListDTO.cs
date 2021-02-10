using CodeShellCore.Data;
using CodeShellCore.Moldster.Db;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.Configurator.Dtos
{
    public class ResourceListDTO : IDTO<Resource>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }

        public static Expression<Func<Resource, ResourceListDTO>> Expression
            => e => new ResourceListDTO
            {
                Id = e.Id,
                Domain = e.DomainId.HasValue ? e.Domain.Name : null,
                Name = e.Name
            };
    }
}
