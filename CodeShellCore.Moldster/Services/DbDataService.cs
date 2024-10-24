using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Services
{
    public class DbDataService : ApplicationService, IDataService
    {
        private readonly IConfigUnit _unit;

        public DbDataService(IServiceProvider provider, IConfigUnit unit) : base(provider)
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

        public PageOptionsDto GetPageOptions(string moduleCode, string viewPath)
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
            return _unit.TenantRepository.FindSingleAndMap<TenantPageGuideDTO>(id);
        }

        public async Task<IEnumerable<PageOptionsDto>> GetPageOptionsByCategory(long categoryId, long tenantId)
        {
            List<PageOptions> pages = await _unit.PageRepository.GetPageOptionsByCategory(categoryId, 9);
            return Mapper.Map(pages, new List<PageOptionsDto>());
        }

        public PageOptionsDto GetPageOptionsById(long pageId)
        {
            PageOptionsDto opts = _unit.PageRepository.FindSingleAs(d => new PageOptionsDto
            {
                PageId = pageId,
                PageIdentifier = d.Domain.Name + "__" + d.Name,
                ViewParamsString = d.ViewParams,
                Layout = d.Layout + ".cshtml",
                ViewPath = d.PageCategory.ViewPath,
                DefaultAccessibility = d.DefaultAccessibility,
            }, e => e.Id == pageId);

            var lst = _unit.PageControlRepository.GetDtos<ControlRenderDto>(e => e.PageId == pageId);

            opts.Controls = new Dictionary<string, ControlRenderDto>();
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

        public Task<PageOptionsDto> GetCategoryPageOptions(long pageCategoryId)
        {
            return Task.Run(async () =>
            {
                PageOptionsDto opts = _unit.PageCategoryRepository.FindSingleAs(d => new PageOptionsDto
                {
                    PageId = 0,
                    PageIdentifier = d.Domain.Name + "__" + d.Name,
                    //Layout = !string.IsNullOrEmpty(d.Layout) ? $"Layout/{d.Layout}Layout.cshtml" : "Layout/DefaultLayout.cshtml",
                    Layout = "Layout/DynamicLayout.cshtml",
                    ViewPath = d.ViewPath,
                    DefaultAccessibility = 2,
                }, e => e.Id == pageCategoryId);

                var lst = _unit.ControlRepository.FindAndMap<ControlRenderDto>(e => e.PageCategoryId == pageCategoryId);
                var defaultEmbedded = await _unit.PageCategoryParameterRepository.GetListAsync(e => e.PageCategoryId == pageCategoryId && e.Type == 2);

                var prms = new ViewParams();
                foreach (var item in defaultEmbedded)
                {
                    prms.Other[item.Name] = item.DefaultValue;
                }
                opts.SetViewParams(prms);
                opts.Controls = new Dictionary<string, ControlRenderDto>();
                var rep = new List<string>();
                foreach (var d in lst)
                {
                    if (opts.Controls.ContainsKey(d.Identifier))
                        rep.Add(d.Identifier);

                    opts.Controls[d.Identifier] = d;
                }
                opts.RepeatedIds = rep;

                return opts;
            });

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
