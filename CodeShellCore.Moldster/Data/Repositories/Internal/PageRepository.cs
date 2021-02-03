using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CodeShellCore.Moldster.Data.Repositories.Internal
{
    public class PageRepository : Repository_Int64<Page, MoldsterContext>, IPageRepository
    {
        private readonly IPathsService paths;

        public PageRepository(MoldsterContext con, IPathsService paths) : base(con)
        {
            this.paths = paths;
        }


        protected IQueryable<PageDTO> QueryPageDTOForRendering(IQueryable<Page> q = null)
        {
            q = q ?? Loader;
            return q.Select(e => new PageDTO
            {
                TenantCode = e.Tenant.Code,
                TenantId = e.TenantId,
                Page = e,
                BaseScript = paths.CoreAppName + "/" + e.PageCategory.ViewPath + "Base",
                ParentHasResource = e.PageCategory.ResourceId != null,
                ResourceName = e.Resource.Name,
                DomainName = e.Domain.Name,
                CollectionId = e.SourceCollection == null ? null : e.SourceCollection.Name,
            });

        }

        protected IQueryable<PageDTO> QueryPageDTOForRouting(IQueryable<Page> q = null)
        {
            q = q ?? Loader;
            return q.Select(p => new PageDTO
            {
                DomainName = p.Domain.Name,
                TenantCode = p.Tenant.Code,
                TenantId = p.TenantId,
                Page = p,
                BaseScript = paths.CoreAppName + "/" + p.PageCategory.ViewPath + "Base",
                ResourceName = p.Resource.Name,
                ActionName = p.ResourceAction == null ? (p.SpecialPermission ?? null) : p.ResourceAction.Name,
                PageIdentifier = p.Domain.Name + "__" + p.Name,
            });
        }

        internal IQueryable<PageListDTO> QueryPageListDTO(IQueryable<Page> q = null)
        {
            q = q ?? Loader;
            var qq = q.Select(PageListDTO.Expression);
            //var qq = from p in q
            //         select new PageListDTO
            //         {
            //             DomainId = p.DomainId,
            //             Id = p.Id,
            //             Name = p.Name,
            //             SpecialPermission = p.SpecialPermission,
            //             TenantId = p.TenantId,
            //             ViewPath = p.ViewPath,
            //             TenantCode = p.Tenant.Code,
            //             BaseComponent = p.PageCategory.BaseComponent,
            //             PageCategoryName = p.PageCategory.Name,
            //             HasRoute = p.HasRoute,
            //             DomainName = p.Domain.Name,
            //             Apps = p.Apps,
            //             RouteParameters = p.RouteParameters
            //         };
            return qq;
        }

        public IEnumerable<PageDTO> GetDomainPagesForRouting(string tenantCode, long domainId)
        {
            var q = Loader.Where(d => d.Tenant.Code == tenantCode && d.DomainId == domainId && d.IsHomePage != true);
            return QueryPageDTOForRouting(q).ToList();
        }

        public string GetHomePagePath(string modCode)
        {
            return GetSingleValue(d => d.ViewPath, d => d.IsHomePage == true && d.Tenant.Code == modCode);
        }

        public PageAndTypeDTO FindLinkedPage(string paramName, string val, long tenantId, ref List<string> add)
        {
            if (val != null)
            {
                if (val[0] == '/')
                    val = val.Substring(1);
                var p = FindSingleAs(d => new PageAndTypeDTO { Id = d.Id, Embedded = d.CanEmbed && !d.HasRoute }, d => d.ViewPath == val && d.TenantId == tenantId);
                if (p == null)
                {
                    p = FindSingleAs(d => new PageAndTypeDTO { Id = d.Id, Embedded = d.CanEmbed && !d.HasRoute }, d => d.Name == val && d.TenantId == tenantId);
                    if (p == null)
                        add.Add("No page with path or name [" + val + "] specified in " + paramName);
                }
                return p;
            }
            return null;
        }

        public PageAndTypeDTO FindLinkedPageByName(string paramName, string val, long tenantId, ref List<string> add)
        {
            if (val != null)
            {
                if (val[0] == '/')
                    val = val.Substring(1);
                var p = FindSingleAs(d => new PageAndTypeDTO { Id = d.Id, Embedded = d.CanEmbed && !d.HasRoute }, d => d.Name == val && d.TenantId == tenantId);
                if (p == null)
                    add.Add("No page with name [" + val + "] specified in " + paramName);
                else
                    return p;
            }
            return null;
        }

        public LoadResult<PageListDTO> GetUnderDomain(long domainId, LoadOptions opt)
        {
            var opts = opt.GetOptionsFor<PageListDTO>();
            var q = from p in Loader
                    where p.Domain.Chain.Contains("|" + domainId.ToString() + "|")
                    select p;
            var qq = QueryPageListDTO(q);
            return qq.LoadWith(opts);
        }

        public LoadResult<PageListDTO> FindUsing(FindPageRequest request, LoadOptions opts)
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
            return QueryPageListDTO(q).LoadWith(opts.GetOptionsFor<PageListDTO>());
        }

        public void UpdatePageViewParamsJson(Page p, PageParameterForJson[] ps, PageRouteDTO r, FieldDefinition[] definitions)
        {

            var jsonParams = p.ViewParams == null ? new ViewParams() : p.ViewParams.FromJson<ViewParams>();
            if (r != null)
            {
                jsonParams.AddUrl = r.AddUrlString != null ? "/" + r.AddUrlString : null;
                jsonParams.DetailsUrl = r.DetailsUrlString != null ? "/" + r.DetailsUrlString : null;
                jsonParams.EditUrl = r.EditUrlString != null ? "/" + r.EditUrlString : null;
                jsonParams.ListUrl = r.ListUrlString != null ? "/" + r.ListUrlString : null;
            }
            foreach (var pp in ps)
            {
                jsonParams.Other[pp.Name] = pp.Value;
            }
            if (definitions != null)
            {
                jsonParams.Fields = definitions;
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

        public void FillReferences(IEnumerable<PageListDTO> listT)
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

        public void FillReferencedBy(IEnumerable<PageListDTO> listT)
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

        public PageDTO FindSingleForRendering(Expression<Func<Page, bool>> p)
        {
            var q = Loader.Where(p);
            return QueryPageDTOForRendering(q).FirstOrDefault();
        }

        public List<PageIdentifierDTO> GetDistinctIdentifiers()
        {
            return Loader
                 .GroupBy(e => new { DomainName = e.Domain.Name, Page = e.Name })
                 .Select(e => new PageIdentifierDTO { Domain = e.Key.DomainName, Page = e.Key.Page })
                 .ToList();
        }
    }
}
