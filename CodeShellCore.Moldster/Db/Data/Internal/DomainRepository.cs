﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Services.Recursive;

namespace CodeShellCore.Moldster.Db.Data.Internal
{
    public class DomainRepository : MoldsterRepository<Domain, MoldsterContext>, IDomainRepository,IRecursiveRepository<Domain>
    {
        DefaultRecursiveRepository<Domain, MoldsterContext> _recRepo; 
        public DomainRepository(MoldsterContext con) : base(con)
        {
            _recRepo = new DefaultRecursiveRepository<Domain, MoldsterContext>(con);
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
        public Domain GetOrCreatePath(string dom)
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
                Domain domain = FindSingle(d => d.NameChain == searchTerm + "/");
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

        public IEnumerable<RecursionModel> GetRecursionModels()
        {
            return _recRepo.GetRecursionModels();
        }

        public IEnumerable<RecursionModel> GetRecursionModels(Expression<Func<Domain, bool>> filter)
        {
            return _recRepo.GetRecursionModels();
        }

        public IEnumerable<Domain> GetChildren(object prime)
        {
            return _recRepo.GetChildren(prime);
        }

        public IEnumerable<Domain> GetChildren(object prime, Expression<Func<Domain, bool>> filter)
        {
            return _recRepo.GetChildren(prime, filter);
        }

        public IEnumerable<Domain> GetRooted(Expression<Func<Domain, bool>> filter)
        {
            return _recRepo.GetRooted(filter);
        }

        public void DeleteAllSubs(object prime)
        {
            _recRepo.DeleteAllSubs(prime);
        }
    }
}