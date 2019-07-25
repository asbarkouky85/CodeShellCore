using CodeShellCore.Moldster.Db.Data;
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
            return _unit.PageRepository.GetValues(
                d => d.ViewPath,
                d => d.TenantDomain.Domain.Name == domain && d.TenantDomain.Tenant.Code == mod
                ).ToArray();
        }

        public string[] GetModuleDomains(string modCode)
        {
            return _unit.TenantDomainRepository.GetValues(d => d.Domain.Name, d => d.Tenant.Code == modCode).ToArray();
        }

        public string[] GetModuleNames(bool? active = null)
        {
            return _unit.TenantRepository.GetValues(d => d.Name, d => d.IsActive == active || active == null).ToArray();
        }

        public PageOptions GetPageOptions(string moduleCode, string viewPath)
        {
            long pageId = _unit.PageRepository.GetSingleValue(
                d => d.Id,
                d => d.TenantDomain.Tenant.Code == moduleCode && d.ViewPath == viewPath);
            return _controls.GetPageOptions(pageId);
        }

        public string[] GetTemplatePaths(string modCode, string domain = null)
        {
            return _unit.PageCategoryRepository.GetValues(
                d => d.ViewPath,
                d => d.Pages.Any(e =>
                    e.TenantDomain.Tenant.Code == modCode &&
                    (e.TenantDomain.Domain.Name == domain || domain == null)
                )).ToArray();
        }
    }
}
