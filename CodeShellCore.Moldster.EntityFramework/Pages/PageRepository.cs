using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Pages.Views;
using CodeShellCore.Text;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Moldster.Pages
{
    public class PageRepository : Repository_Int64<Page, MoldsterContext>, IPageRepository
    {
        public PageRepository(MoldsterContext con) : base(con)
        {

        }

        public IEnumerable<T> GetDomainPagesForRouting<T>(string tenantCode, long domainId, bool chldren = false)
        {
            var q = Loader.Where(d => d.Tenant.Code == tenantCode && d.DomainId == domainId && d.IsHomePage != true);
            if (chldren)
            {
                q = Loader.Where(d => d.Tenant.Code == tenantCode && d.Domain.Chain.Contains("|" + domainId + "|") && d.IsHomePage != true);
            }
            return QueryDto<T>(q).ToList();
        }

        public string GetHomePagePath(string modCode)
        {
            return GetSingleValue(d => d.ViewPath, d => d.IsHomePage == true && d.Tenant.Code == modCode);
        }

        public override Page FindSingle(object id)
        {
            var q = Loader.Include(e => e.Domain)
                .Include(e => e.NavigationPages)
                .Include(e => e.Resource)
                .Include(e => e.PageCategory)
                .Include(e => e.Tenant);

            return q.Where(e => e.Id.Equals(id)).FirstOrDefault();
        }

        public PageAndType FindLinkedPage(string paramName, string val, long tenantId, ref List<string> add)
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

        public PageAndType FindLinkedPageByName(string paramName, string val, long tenantId, ref List<string> add)
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

        public LoadResult<T> GetUnderDomain<T>(long domainId, LoadOptions opt) where T : class
        {
            var opts = opt.GetOptionsFor<T>();
            var q = from p in Loader
                    where p.Domain.Chain.Contains("|" + domainId.ToString() + "|")
                    select p;
            var qq = QueryDto<T>(q);
            return qq.LoadWith(opts);
        }

        public LoadResult<T> FindUsing<T>(FindPageRequest request, LoadOptions opts) where T : class
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
            return QueryDto<T>(q).LoadWith(opts.GetOptionsFor<T>());
        }

        public void UpdatePageViewParamsJson(Page p, PageParameterForJson[] ps, PageRouteView pageRoute, FieldDefinition[] customFields)
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
        }

        public IEnumerable<Page> GetReferencing(long pageId, long tenantId)
        {
            var q = from p in Loader
                    where p.TenantId == tenantId &&
                    (
                        p.PageRoutes.Any(d => d.EditUrl == pageId || d.AddUrl == pageId || d.DetailsUrl == pageId || d.ListUrl == pageId) ||
                        p.PageParameters.Any(d => d.LinkedPageId == pageId)
                    )
                    select p;

            return q.ToList();
        }

        public void FillReferences(IEnumerable<IPageReferenceCounter> listT)
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
        }

        public void FillReferencedBy(IEnumerable<IPageReferenceCounter> listT)
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
        }

        public List<PageIdentifierView> GetDistinctIdentifiers()
        {
            return Loader
                 .GroupBy(e => new { DomainName = e.Domain.Name, Page = e.Name })
                 .Select(e => new PageIdentifierView { Domain = e.Key.DomainName, Page = e.Key.Page })
                 .ToList();
        }

        public Page GetForCustomization(long id)
        {
            var q = Loader
                .Include(e => e.PageControls)
                .Include(e => e.PageParameters)
                .Include(e => e.PageRoutes)
                .Include(e => e.CustomFields);
            return q.FirstOrDefault(e => e.Id == id);
        }
    }
}
