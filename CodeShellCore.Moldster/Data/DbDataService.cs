using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CodeShellCore.Moldster.Pages.Dtos;

namespace CodeShellCore.Moldster.Data.Internal
{
    public class DbDataService : ServiceBase, IDataService
    {
        private readonly IConfigUnit _unit;

        public DbDataService(IConfigUnit unit)
        {
            _unit = unit;
        }
        public PageRenderDTO[] GetDomainPagesForRendering(string mod, string domain, bool recursive = true)
        {
            if (string.IsNullOrEmpty(domain))
                return new PageRenderDTO[0];
            var query = domain;
            query = query[0] != '/' ? "/" + query : query;
            query = query[query.Length - 1] != '/' ? query + "/" : query;

            if (recursive)
            {
                return _unit.PageRepository.GetValues(
                d => new PageRenderDTO { ViewPath = d.ViewPath, Id = d.Id },
                d =>
                    d.Domain.NameChain.Contains(query) &&
                    d.Tenant.Code == mod
                ).ToArray();
            }
            else
            {
                return _unit.PageRepository.GetValues(
                d => new PageRenderDTO { ViewPath = d.ViewPath, Id = d.Id },
                d =>
                    d.Domain.NameChain == query &&
                    d.Tenant.Code == mod
                ).ToArray();
            }

        }

        public IEnumerable<DomainRecursive> GetModuleDomains(string modCode)
        {
            Expression<Func<Domain, bool>> ex = null;
            if (modCode != null)
                ex = d => d.Pages.Any(e => e.Tenant.Code == modCode);
            var doms = _unit.DomainRepository.GetRooted(ex).Recurse();
            List<DomainRecursive> lst = new List<DomainRecursive>();
            foreach (var d in doms)
            {
                lst.Add(DomainRecursive.ToDomainRecursive(d));
            }
            return lst;
        }

        public string[] GetAppCodes(bool? active = null)
        {
            return _unit.TenantRepository.GetValues(d => d.Code, d => d.IsActive == active || active == null).ToArray();
        }

        public PageOptions GetPageOptions(string moduleCode, string viewPath)
        {
            long pageId = _unit.PageRepository.GetSingleValue(
                d => d.Id,
                d => d.Tenant.Code == moduleCode && d.ViewPath == viewPath);
            return GetPageOptionsById(pageId);
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

        public TenantPageGuideDTO GetAppGuide(long id)
        {
            return _unit.TenantRepository.FindSingleAs(TenantPageGuideDTO.Expression, id);
        }

        public PageOptions GetPageOptionsById(long pageId)
        {
            List<ControlDTO> lst = new List<ControlDTO>();

            PageOptions opts = _unit.PageRepository.FindSingleAs(d => new PageOptions
            {
                PageId = pageId,
                PageIdentifier = d.Domain.Name + "__" + d.Name,
                ViewParamsString = d.ViewParams,
                Layout = d.Layout + ".cshtml",
                ViewPath = d.PageCategory.ViewPath,
                DefaultAccessibility = d.DefaultAccessibility,
            }, e => e.Id == pageId);
            lst = _unit.PageControlRepository.GetDtos(e => e.PageId == pageId);

            opts.Controls = new Dictionary<string, ControlDTO>();
            var rep = new List<string>();
            foreach (var d in lst)
            {
                if (opts.Controls.ContainsKey(d.Identifier))
                    rep.Add(d.Identifier);

                opts.Controls[d.Identifier] = d;
            }
            opts.RepeatedIds = rep;

            return opts;
        }

        public string GetAppStyle(string modCode)
        {
            return _unit.TenantRepository.GetSingleValue(d => d.BaseStyle, d => d.Code == modCode);
        }

        public string GetAppVersion(string code)
        {
            return _unit.TenantRepository.GetSingleValue(d => d.Version, d => d.Code == code);
        }

        public SubmitResult SetAppVersion(string code, string version)
        {
            var ten = _unit.TenantRepository.FindSingle(d => d.Code == code);
            if (ten != null)
                ten.Version = version;
            return _unit.SaveChanges();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
