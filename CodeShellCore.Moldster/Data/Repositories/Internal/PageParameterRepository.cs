using CodeShellCore.Linq;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Pages;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Moldster.Data.Repositories.Internal
{
    public class PageParameterRepository : MoldsterRepository<PageParameter, MoldsterContext>, IPageParameterRepository
    {
        private readonly INamingConventionService names;

        public PageParameterRepository(MoldsterContext con, INamingConventionService srv) : base(con)
        {
            this.names = srv;
        }

        internal IQueryable<PageReferenceDTO> QueryPageReferenceDTO(IQueryable<PageParameter> q = null)
        {
            var pages = DbContext.PageParameters;
            q = q ?? Loader;
            return from d in q
                   select new PageReferenceDTO
                   {
                       PageId = d.PageId,
                       PageCategoryId = d.PageCategoryParameter.PageCategoryId,
                       PageCategoryName = d.PageCategoryParameter.PageCategory.Name,
                       PageCategoryViewPath = d.PageCategoryParameter.PageCategory.ViewPath,
                       PageName = d.Page.Name,
                       PageViewPath = d.Page.ViewPath,
                       ParameterName = d.PageCategoryParameter.Name,
                       ReferencedPageId = d.LinkedPageId,
                       ReferencedPageName = d.LinkedPageId == null ? null : DbContext.Pages.Where(e => e.Id == d.LinkedPageId).Select(e => e.Name).FirstOrDefault(),
                       ReferencedPageViewPath = d.LinkedPageId == null ? null : DbContext.Pages.Where(e => e.Id == d.LinkedPageId).Select(e => e.ViewPath).FirstOrDefault(),
                       ReferenceTypeId = d.PageCategoryParameter.Type
                   };
        }

        internal IQueryable<PageParameterForJson> QueryPageParameterForJson(IQueryable<PageParameter> q = null)
        {
            var textType = (long)PageParameterTypes.Text;
            var pages = DbContext.Pages;
            q = q ?? Loader;
            return from d in q
                   select new PageParameterForJson
                   {
                       PageId = d.PageId,
                       Name = d.PageCategoryParameter.Name,
                       Value = d.PageCategoryParameter.Type == textType ?
                       d.ParameterValue : (d.LinkedPageId.HasValue ?
                       pages.Where(e => e.Id == d.LinkedPageId).Select(e => e.ViewPath).FirstOrDefault() : null),
                       Type = d.PageCategoryParameter.Type
                   };
        }

        public IEnumerable<PageParameterForJson> FindForJsonByPage(long pageId)
        {
            var q = Loader.Where(d => d.PageId == pageId);

            var data = QueryPageParameterForJson(q).ToList();
            foreach (var item in data)
            {
                if (item.Value != null && item.Type != (int)PageParameterTypes.Text)
                {
                    switch ((PageParameterTypes)item.Type)
                    {
                        case PageParameterTypes.Embedded:
                            item.Value = names.GetComponentSelector(item.Value);
                            break;
                        case PageParameterTypes.PageLink:
                            item.Value = names.ApplyConvension(item.Value, AppParts.Route);
                            break;
                    }

                }
            }
            return data;
        }

        public IEnumerable<PageParameterForJson> FindForJson(long tenantId, long? pageCategoryId = null)
        {
            var q = Loader.Where(d => (d.Page.PageCategoryId == pageCategoryId || pageCategoryId == null) && d.Page.TenantId == tenantId);
            return QueryPageParameterForJson(q).ToList();
        }

        public LoadResult<PageReferenceDTO> FindReferences(ParameterRequestDTO req, ListOptions<PageReferenceDTO> o)
        {
            var q = Loader.Where(d => d.Page.Tenant.Code == req.TenantCode);

            if (req.ParameterTypeId != null)
            {
                q = q.Where(d => d.PageCategoryParameter.Type == req.ParameterTypeId.Value);
            }

            if (req.Type != null)
            {
                switch (req.Type.Value)
                {
                    case ParameterRequestTypes.InvalidLinks:
                        q = from par in q
                            where
                                !par.Page.Domain.Chain.Contains("|" + DbContext.Pages.Where(e => e.Id == par.LinkedPageId).Select(e => e.DomainId).FirstOrDefault() + "|") &&
                                DbContext.Pages.Where(e => e.Id == par.LinkedPageId).Select(e => e.Domain.Name).FirstOrDefault() != "Shared" &&
                                par.PageCategoryParameter.Type != 3
                            select par;
                        break;
                    case ParameterRequestTypes.MissingLinks:
                        q = q.Where(d => d.LinkedPageId == null && d.PageCategoryParameter.Type != 1);
                        break;
                }
            }

            if (req.ReferencedByPageId != null)
                q = q.Where(d => d.PageId == req.ReferencedByPageId.Value);

            else if (req.ReferencedPageId != null)
                q = q.Where(d => d.LinkedPageId == req.ReferencedPageId.Value);

            return QueryPageReferenceDTO(q).LoadWith(o);
        }

        public List<PageReference> GetReferencesByPage(long id)
        {
            var q = Loader.Where(e => e.PageId == id && e.LinkedPageId != null);
            var pagesQuery = DbContext.Pages;
            return q.Select(e => new PageReference
            {
                PageParameterId = e.Id,
                ViewPath = pagesQuery.Where(d => d.Id == e.LinkedPageId).Select(d => d.ViewPath).FirstOrDefault()
            }).ToList();


        }
    }
}
