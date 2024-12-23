using CodeShellCore.Data.Recursion;
using CodeShellCore.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Domains
{
    public class DomainRepository : MoldsterRepository<Domain, MoldsterContext>, IDomainRepository, IRecursiveRepository<Domain, DefaultRecursionModel>
    {
        DefaultRecursiveRepository<Domain, DefaultRecursionModel, MoldsterContext> _recRepo;
        public DomainRepository(MoldsterContext con) : base(con)
        {
            _recRepo = new DefaultRecursiveRepository<Domain, DefaultRecursionModel, MoldsterContext>(con);
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

        internal IQueryable<Domain> QueryByTenant(long id, IQueryable<Domain> q = null)
        {
            q = q ?? Loader;
            return from d in Loader
                   where (from p in DbContext.Pages
                          where p.Domain.Chain.Contains("|" + d.Id + "|")
                          select p.TenantId).Contains(id)
                   select d;
        }

        internal IQueryable<Domain> QueryHavingCategories(IQueryable<Domain> q = null)
        {
            q = q ?? Loader;
            return from d in Loader
                   where (from p in DbContext.PageCategories
                          where p.Domain.Chain.Contains("|" + d.Id + "|")
                          select p.Id).Any()
                   select d;
        }

        public async Task<List<T>> GetByTenantCodeForRouting<T>(string moduleCode, long? domId = null) where T : class
        {
            //if (domId == 1)
            //{
            //    return new List<DomainWithPagesDTO> { new DomainWithPagesDTO { Id = 1, DomainName = "Shared", ParentId = null } };
            //}
            var q = from d in Loader
                    where (domId == null || d.Chain.Contains("|" + domId + "|"))
                    && DbContext.Pages.Any(e => e.Domain.Chain.Contains("|" + d.Id + "|") && e.Tenant.Code == moduleCode)
                    select d;



            return await QueryDto<T>(q).ToListAsync();
        }

        public Task<Domain> GetDomainByPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException();
            if (path[0] != '/')
                path = "/" + path;
            if (path[path.Length - 1] != '/')
                path += "/";
            return FindSingle(d => d.NameChain == path);
        }

        public async Task<List<T>> GetParentModules<T>(long modId)
        {
            var q = QueryByTenant(modId).Where(d => d.ParentId == null);
            return await QueryDto<T>(q).ToListAsync();
        }

        List<string> _partitionPath(string path)
        {
            string[] spl = path.Split('/');
            List<string> lst = new List<string>();
            foreach (var s in spl)
            {
                if (!string.IsNullOrEmpty(s))
                    lst.Add(s.Trim());
            }
            return lst;
        }

        /// <summary>
        /// id is returned in the data dictionary as LastId
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        public async Task<Domain> GetOrCreatePath(string dom)
        {
            if (dom[0] != '/')
                dom = "/" + dom;

            var parts = _partitionPath(dom);
            string searchTerm = "";
            List<Domain> domains = new List<Domain>();

            Domain last = null;
            foreach (var part in parts)
            {
                searchTerm += "/" + part;
                Domain domain = await FindSingle(d => d.NameChain == searchTerm + "/");
                if (domain == null)
                {
                    domain = new Domain
                    {
                        Id = Utils.GenerateID(),
                        Name = part,
                        ParentId = last?.Id
                    };

                    Add(domain);
                }
                last = domain;
            }
            return last;

        }

        public async Task<Domain> GetOrCreatePath(string dom, List<Domain> doms)
        {
            if (dom[0] != '/')
                dom = "/" + dom;

            var parts = _partitionPath(dom);
            string searchTerm = "";

            Domain last = null;
            foreach (var part in parts)
            {
                searchTerm += "/" + part;
                Domain domain = await FindSingle(d => d.NameChain == searchTerm + "/");
                domain = domain ?? doms.FirstOrDefault(d => d.Name == part);
                if (domain == null)
                {
                    domain = new Domain
                    {
                        Id = Utils.GenerateID(),
                        Name = part,
                        ParentId = last?.Id
                    };

                    Add(domain);
                    doms.Add(domain);
                }
                last = domain;
            }
            return last;

        }

        public Task<IEnumerable<DefaultRecursionModel>> GetRecursionModels()
        {
            return _recRepo.GetRecursionModels();
        }

        public Task<IEnumerable<DefaultRecursionModel>> GetRecursionModels(Expression<Func<Domain, bool>> filter)
        {
            return _recRepo.GetRecursionModels();
        }

        public Task<IEnumerable<Domain>> GetChildren(object prime)
        {
            return _recRepo.GetChildren(prime);
        }

        public Task<IEnumerable<Domain>> GetChildren(object prime, Expression<Func<Domain, bool>> filter)
        {
            return _recRepo.GetChildren(prime, filter);
        }

        public Task<IEnumerable<Domain>> GetRooted(Expression<Func<Domain, bool>> filter)
        {
            return _recRepo.GetRooted(filter);
        }

        public async Task DeleteAllSubs(object prime)
        {
            await _recRepo.DeleteAllSubs(prime);
        }

        public async Task<List<Domain>> GetHavingPagesForTenant(long value)
        {
            return await QueryByTenant(value).ToListAsync();
        }

        public async Task<List<Domain>> GetHavingCategories()
        {
            return await QueryHavingCategories().ToListAsync();
        }
    }
}
