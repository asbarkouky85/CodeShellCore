using CodeShellCore.Security;
using CodeShellCore.Security.Authorization;
using System.Collections.Generic;
using System.Linq;
using CodeShellCore.Data.ConfiguredCollections;
using Asga.Auth.Dto;
using System;
using System.Linq.Expressions;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Text;

namespace Asga.Auth.Data
{
    public class ResourceRepository : AsgaRepository<Resource, AuthContext>, IResourceRepository, IAsgaResourceRepository
    {
        public ResourceRepository(AuthContext con, ICollectionConfigService service, IUserAccessor acc) : base(con, service, acc)
        {
        }

        protected bool IsCollection(string identifier, out string collId)
        {
            collId = null;
            if (string.IsNullOrEmpty(identifier))
            {
                return false;
            }
            else if (identifier.Contains("__"))
            {
                collId = identifier.GetAfterLast("__");
            }
            else
            {
                collId = identifier;
            }
            return true;
        }

        protected IQueryable<ResourceWithActionsDTO> QueryResourceWithActionsDTO(IQueryable<Resource> q = null)
        {
            q = q ?? Loader;
            return q.Select(d => new ResourceWithActionsDTO
            {
                Actions = d.ResourceActions.Select(e => new Named<long> { Id = e.Id, Name = e.Name }),
                Collections = d.ResourceCollections.Select(e => new Named<long> { Id = e.Id, Name = e.Name }),
                Id = d.Id,
                Name = d.Name
            });
        }

        public IEnumerable<ResourceWithActionsDTO> GetResourcesWithActions(string collectionId = null, Expression<Func<Resource, bool>> ex = null)
        {
            var q = Loader;
            if (IsCollection(collectionId, out string coll))
                q = QueryCollection(coll);
            if (ex != null)
                q = q.Where(ex);

            return QueryResourceWithActionsDTO(q).ToList();
        }

        public virtual IEnumerable<DomainWithResourcesDTO> GetClassifiedByDomain(string collectionId = null, Expression<Func<Resource, bool>> ex = null)
        {
            var q = string.IsNullOrEmpty(collectionId) ? Loader : QueryCollection(collectionId);
            if (ex != null)
                q = q.Where(ex);

            var dQ = from d in DbContext.Domains
                     where q.Select(r => r.DomainId).Contains(d.Id)
                     select d;

            return dQ.Select(e => new DomainWithResourcesDTO
            {
                Id = e.Id,
                Name = e.Name,
                Resources = from n in q.Where(r => r.DomainId == e.Id)
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
            }).ToList();
        }

        public virtual string[] GetResourcesWithCollections()
        {
            return DbContext.Resources.Where(d => d.ResourceCollections.Any()).Select(d => d.Name).ToArray();
        }

        public virtual List<ResourceActionV> GetRoleResourceActions(object roleId)
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

        public virtual List<ResourceV> GetRoleResources(object roleId)
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
