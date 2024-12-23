using Azure;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Extensions.DependencyInjection;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages.Views;
using CodeShellCore.Text;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public class PageRepository : Repository_Int64<Page, MoldsterContext>, IPageRepository
    {
        public PageRepository(MoldsterContext con) : base(con)
        {

        }

        public async Task<IEnumerable<T>> GetDomainPagesForRouting<T>(string tenantCode, long domainId, bool chldren = false)
        {
            var q = Loader.Where(d => d.Tenant.Code == tenantCode && d.DomainId == domainId && d.IsHomePage != true);
            if (chldren)
            {
                q = Loader.Where(d => d.Tenant.Code == tenantCode && d.Domain.Chain.Contains("|" + domainId + "|") && d.IsHomePage != true);
            }
            return await QueryDto<T>(q).ToListAsync();
        }

        public Task<string> GetHomePagePath(string modCode)
        {
            return GetSingleValue(d => d.ViewPath, d => d.IsHomePage == true && d.Tenant.Code == modCode);
        }

        public override async Task<Page> FindSingle(object id)
        {
            var q = Loader.Include(e => e.Domain)
                .Include(e => e.NavigationPages)
                .Include(e => e.Resource)
                .Include(e => e.PageCategory)
                .Include(e => e.Tenant);

            return await q.Where(e => e.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public Task<PageAndType> FindLinkedPage(string paramName, string val, long tenantId, ref List<string> add)
        {
            if (val != null)
            {
                if (val[0] == '/')
                    val = val.Substring(1);
                var p = FindSingleAs(d => new PageAndType { Id = d.Id, Embedded = d.CanEmbed && !d.HasRoute }, d => d.ViewPath == val && d.TenantId == tenantId);
                if (p == null)
                {
                    p = FindSingleAs(d => new PageAndType { Id = d.Id, Embedded = d.CanEmbed && !d.HasRoute }, d => d.Name == val && d.TenantId == tenantId);
                    if (p == null)
                        add.Add("No page with path or name [" + val + "] specified in " + paramName);
                }
                return p;
            }
            return null;
        }

        public Task<PageAndType> FindLinkedPageByName(string paramName, string val, long tenantId, ref List<string> add)
        {
            if (val != null)
            {
                if (val[0] == '/')
                    val = val.Substring(1);
                var p = FindSingleAs(d => new PageAndType { Id = d.Id, Embedded = d.CanEmbed && !d.HasRoute }, d => d.Name == val && d.TenantId == tenantId);
                if (p == null)
                    add.Add("No page with name [" + val + "] specified in " + paramName);
                else
                    return p;
            }
            return null;
        }

        public async Task<PagedResult<T>> GetUnderDomain<T>(long domainId, PagedListRequest opt) where T : class
        {
            var opts = opt.GetOptionsFor<T>();
            var q = from p in Loader
                    where p.Domain.Chain.Contains("|" + domainId.ToString() + "|")
                    select p;
            var qq = QueryDto<T>(q);
            return await qq.ToPagedResultAsync(opts);
        }

        public async Task<PagedResult<T>> FindUsing<T>(FindPageRequest request, PagedListRequest opts) where T : class
        {
            var q = Loader.Where(d => d.TenantId == request.TenantId);
            switch (request.TypeEnum)
            {
                case PageTypes.AnyRoutable:
                    q = q.Where(d => d.HasRoute);
                    break;
                case PageTypes.ParameterizedRoutable:
                    q = q.Where(d => d.HasRoute && d.RouteParameters != null);
                    break;
                case PageTypes.UnParameterizedRoutable:
                    q = q.Where(d => d.HasRoute && d.RouteParameters == null);
                    break;
                case PageTypes.Embedded:
                    q = q.Where(d => d.CanEmbed);
                    break;
            }
            return await QueryDto<T>(q).ToPagedResultAsync(opts.GetOptionsFor<T>());
        }

        public Task UpdatePageViewParamsJson(Page p, PageParameterForJson[] ps, PageRouteView pageRoute, FieldDefinition[] customFields)
        {
            return Task.Run(() =>
            {

                var jsonParams = p.ViewParams == null ? new ViewParams() : p.ViewParams.FromJson<ViewParams>();
                if (pageRoute != null)
                {
                    jsonParams.AddUrl = pageRoute.AddUrlString != null ? "/" + pageRoute.AddUrlString : null;
                    jsonParams.DetailsUrl = pageRoute.DetailsUrlString != null ? "/" + pageRoute.DetailsUrlString : null;
                    jsonParams.EditUrl = pageRoute.EditUrlString != null ? "/" + pageRoute.EditUrlString : null;
                    jsonParams.ListUrl = pageRoute.ListUrlString != null ? "/" + pageRoute.ListUrlString : null;
                }
                foreach (var pp in ps)
                {
                    jsonParams.Other[pp.Name] = pp.Value;
                }
                if (customFields != null)
                {
                    jsonParams.Fields = customFields;
                }
                p.ViewParams = jsonParams.ToJson();
                Update(p);
            });
        }

        public async Task<IEnumerable<Page>> GetReferencing(long pageId, long tenantId)
        {
            var q = from p in Loader
                    where p.TenantId == tenantId &&
                    (
                        p.PageRoutes.Any(d => d.EditUrl == pageId || d.AddUrl == pageId || d.DetailsUrl == pageId || d.ListUrl == pageId) ||
                        p.PageParameters.Any(d => d.LinkedPageId == pageId)
                    )
                    select p;

            return await q.ToListAsync();
        }

        public Task FillReferences(IEnumerable<IPageReferenceCounter> listT)
        {
            return Task.Run(() =>
            {
                var ids = listT.Select(d => d.Id).ToList();
                var q = from p in DbContext.PageRoutes
                        where ids.Contains(p.Page.Id)
                        select new
                        {
                            p.Id,
                            Edit = p.EditUrl != null ? 1 : 0,
                            Add = p.AddUrl != null ? 1 : 0,
                            Details = p.DetailsUrl != null ? 1 : 0,
                            List = p.ListUrl != null ? 1 : 0
                        };

                var qq = from p in DbContext.PageParameters
                         where ids.Contains(p.Page.Id) && p.LinkedPageId != null
                         group p by p.PageId into PS
                         select new { PS.Key, Count = PS.Count() };
                var routs = q.ToList();
                var par = qq.ToList();
                foreach (var page in listT)
                {
                    var pr = routs.Where(d => d.Id == page.Id).FirstOrDefault();
                    var pp = par.Where(d => d.Key == page.Id).FirstOrDefault();
                    if (pr != null)
                        page.References = pr.Edit + pr.Add + pr.Details + pr.List;
                    if (pp != null)
                        page.References += pp.Count;
                }
            });
        }

        public Task FillReferencedBy(IEnumerable<IPageReferenceCounter> listT)
        {
            return Task.Run(() =>
            {

                var ids = listT.Select(d => d.Id).ToList();
                var q = from p in DbContext.Pages
                        where ids.Contains(p.Id)
                        select new
                        {
                            p.Id,
                            RouteRef = DbContext.PageRoutes.Count(d => d.ListUrl == p.Id || d.AddUrl == p.Id || d.EditUrl == p.Id || d.DetailsUrl == p.Id),
                            ParamRef = DbContext.PageParameters.Count(d => d.LinkedPageId == p.Id),
                            NavRefs = DbContext.NavigationPages.Count(d => d.PageId == p.Id)
                        };
                var res = q.ToList();
                foreach (var pp in listT)
                {
                    var pr = res.Where(d => d.Id == pp.Id).FirstOrDefault();
                    if (pr != null)
                    {
                        pp.ReferencedBy = pr.RouteRef + pr.ParamRef + pr.NavRefs;
                    }
                }
            });
        }

        public async Task<List<PageIdentifierView>> GetDistinctIdentifiers()
        {
            return await Loader
                 .GroupBy(e => new { DomainName = e.Domain.Name, Page = e.Name })
                 .Select(e => new PageIdentifierView { Domain = e.Key.DomainName, Page = e.Key.Page })
                 .ToListAsync();
        }

        public async Task<Page> GetForCustomization(long id)
        {
            var q = Loader
                .Include(e => e.PageControls)
                .Include(e => e.PageParameters)
                .Include(e => e.PageRoutes)
                .Include(e => e.CustomFields);
            return await q.FirstOrDefaultAsync(e => e.Id == id);
        }

        private IQueryable<PageOptions> _queryPageOptions(IQueryable<Page> q = null)
        {
            q = q ?? Loader;
            return q.Select(d => new PageOptions
            {
                PageId = d.Id,
                PageIdentifier = d.Domain.Name + "__" + d.Name,
                ViewParamsString = d.ViewParams,
                Layout = d.Layout + ".cshtml",
                ViewPath = d.PageCategory.ViewPath,
                DefaultAccessibility = d.DefaultAccessibility,
            });
        }

        public async Task<List<PageOptions>> GetPageOptionsByCategory(long categoryId, long tenantId)
        {
            var q = _queryPageOptions(Loader.Where(e => e.PageCategoryId == categoryId && e.TenantId == tenantId && e.HasRoute));
            var res = await q.ToListAsync();
            var controls_query = DbContext.PageControls.Where(e => e.Page.TenantId == tenantId && e.Page.PageCategoryId == categoryId);
            var map = await Projector.Project<PageControl, ControlRenderDataObject>(controls_query).ToListAsync();
            foreach (var p in res)
            {
                var pageControls = map.Where(e => e.PageId == p.PageId).ToList();
                foreach (var c in pageControls)
                {
                    p.Controls[c.Identifier] = c;
                }
            }
            return res;
        }
    }
}
