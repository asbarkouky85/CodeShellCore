using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeShellCore.Moldster.Db.Dto;

namespace CodeShellCore.Moldster.Db.Data.Internal
{
    public class DomainRepository : MoldsterRepository<Domain, MoldsterContext>, IDomainRepository
    {
        public DomainRepository(MoldsterContext con) : base(con)
        {
        }

        internal IQueryable<Domain> ByTenantCode(string moduleCode, IQueryable<Domain> q = null)
        {
            q = q ?? Loader;
            return from d in Loader
                   where (from p in DbContext.Pages
                          where p.Domain.Chain.Contains("|" + d.Id + "|")
                          select p.Tenant.Code).Contains(moduleCode)
                   select d;
        }

        internal IQueryable<Domain> ByTenantId(long id, IQueryable<Domain> q = null)
        {
            q = q ?? Loader;
            return from d in Loader
                   where (from p in DbContext.Pages
                          where p.Domain.Chain.Contains("|" + d.Id + "|")
                          select p.TenantId).Contains(id)
                   select d;
        }

        public List<DomainWithPagesDTO> GetByTenantCodeForRouting(string moduleCode, long? domId = null)
        {
            var q = from d in ByTenantCode(moduleCode)
                    where (domId == null || d.Chain.Contains("|" + domId + "|"))
                    select new DomainWithPagesDTO
                    {
                        Id = d.Id,
                        ParentId = d.ParentId,
                        DomainName = d.Name
                    };

            return q.ToList();
        }

        public Domain GetDomainByPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException();
            if (path[0] != '/')
                path = "/" + path;
            if (path[path.Length - 1] != '/')
                path += "/";
            return FindSingle(d => d.NameChain == path);
        }

        public IEnumerable<DomainWithPagesDTO> GetParentModules(long modId)
        {
            return ByTenantId(modId).Where(d => d.ParentId == null).Select(d => new DomainWithPagesDTO
            {
                Id = d.Id,
                ParentId = d.ParentId,
                DomainName = d.Name
            }).ToList();
        }
    }
}
