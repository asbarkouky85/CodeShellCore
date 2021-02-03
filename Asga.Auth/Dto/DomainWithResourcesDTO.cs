using CodeShellCore.Data.Lookups;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;

namespace Asga.Auth.Dto
{
    public class DomainWithResourcesDTO : Named<object>
    {
        public IEnumerable<ResourceWithActionsDTO> Resources { get; set; }

        public static Expression<Func<Domain, DomainWithResourcesDTO>> Expression
        {
            get
            {
                return e => new DomainWithResourcesDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Resources = from n in e.Resources
                                select new ResourceWithActionsDTO
                                {
                                    Id = n.Id,
                                    Name = n.Name,
                                    Actions = from a in n.ResourceActions
                                              select new Named<long>
                                              {
                                                  Id = a.Id,
                                                  Name = a.Name
                                              },
                                    Collections = from b in n.ResourceCollections
                                                  select new Named<long>
                                                  {
                                                      Id = b.Id,
                                                      Name = b.Name
                                                  }
                                }
                };
            }
        }
    }
}
