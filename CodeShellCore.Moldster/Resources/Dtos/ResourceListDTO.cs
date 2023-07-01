using CodeShellCore.Data;
using System;
using System.Linq.Expressions;

namespace CodeShellCore.Moldster.Resources.Dtos
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
