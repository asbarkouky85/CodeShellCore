using CodeShellCore.Data.Lookups;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Moldster.Db.Services
{
    public class DbDataService : ServiceBase, IDataService
    {
        private readonly IConfigUnit _unit;
        private readonly PageControlsService _controls;

        public DbDataService(IConfigUnit unit, PageControlsService controls)
        {
            _unit = unit;
            _controls = controls;
        }
        public string[] GetDomainPages(string mod, string domain)
        {
            if (string.IsNullOrEmpty(domain))
                return new string[0];
            var query = domain;
            query = query[0] != '/' ? "/" + query : query;
            query = query[query.Length - 1] != '/' ? query + "/" : query;
            return _unit.PageRepository.GetValues(
                d => d.ViewPath,
                d => d.Domain.NameChain.Contains(query) && d.Tenant.Code == mod
                ).ToArray();
        }

        public IEnumerable<DomainRecursive> GetModuleDomains(string modCode)
        {
            var doms = _unit.DomainRepository.GetRooted(d => d.Pages.Any(e => e.Tenant.Code == modCode)).Recurse();
            List<DomainRecursive> lst = new List<DomainRecursive>();
            foreach (var d in doms)
            {
                lst.Add(DomainRecursive.ToDomainRecursive(d));
            }
            return lst;
        }

        public string[] GetModuleNames(bool? active = null)
        {
            return _unit.TenantRepository.GetValues(d => d.Name, d => d.IsActive == active || active == null).ToArray();
        }

        public PageOptions GetPageOptions(string moduleCode, string viewPath)
        {
            long pageId = _unit.PageRepository.GetSingleValue(
                d => d.Id,
                d => d.Tenant.Code == moduleCode && d.ViewPath == viewPath);
            return _controls.GetPageOptions(pageId);
        }

        public string[] GetTemplatePaths(string modCode, string domain = null)
        {
            return _unit.PageCategoryRepository.GetValues(
                d => d.ViewPath,
                d => d.Pages.Any(e =>
                    e.Tenant.Code == modCode &&
                    (e.Domain.Name == domain || domain == null)
                )).ToArray();
        }

        public TenantPageGuideDTO GetTenantGuide(long id)
        {
            return _unit.TenantRepository.FindSingleAs(TenantPageGuideDTO.Expression, id);
        }
    }
}
