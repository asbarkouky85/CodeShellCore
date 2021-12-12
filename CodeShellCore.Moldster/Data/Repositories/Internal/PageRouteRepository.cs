using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CodeShellCore.Moldster.Pages.Dtos;
using CodeShellCore.Moldster.Pages;

namespace CodeShellCore.Moldster.Data.Repositories.Internal
{
    public class PageRouteRepository : MoldsterRepository<PageRoute, MoldsterContext>, IPageRouteRepository
    {
        public PageRouteRepository(MoldsterContext con) : base(con)
        {
        }

        protected IQueryable<PageRouteDTO> QueryPageRouteDTO(IQueryable<PageRoute> q = null, long? tenantId = null)
        {
            q = q ?? Loader;
            var pq = DbContext.Pages.AsQueryable();
            // if (tenantId.HasValue)
            //    pq = pq.Where(d => d.TenantId == tenantId);

            var f = from p in q

                    select new PageRouteDTO
                    {
                        PageId = p.PageId,
                        ListUrlString = p.ListUrl.HasValue ? pq.Where(d => d.Id == p.ListUrl).Select(d => d.ViewPath).FirstOrDefault() : null,
                        AddUrlString = p.AddUrl.HasValue ? pq.Where(d => d.Id == p.AddUrl).Select(d => d.ViewPath).FirstOrDefault() : null,
                        EditUrlString = p.EditUrl.HasValue ? pq.Where(d => d.Id == p.EditUrl).Select(d => d.ViewPath).FirstOrDefault() : null,
                        DetailsUrlString = p.DetailsUrl.HasValue ? pq.Where(d => d.Id == p.DetailsUrl).Select(d => d.ViewPath).FirstOrDefault() : null,
                        AddUrl = p.AddUrl,
                        ListUrl = p.ListUrl,
                        DetailsUrl = p.ListUrl,
                        EditUrl = p.EditUrl,
                        Id = p.Id
                    };
            return f;
        }

        public IEnumerable<PageRouteDTO> FindForJson(long tenantId, long? categoryId = null)
        {
            var f = QueryPageRouteDTO(Loader.Where(p => (p.Page.PageCategoryId == categoryId || categoryId == null) && p.Page.TenantId == tenantId), tenantId);
            return f.ToList();
        }

        public PageRouteDTO FindByPage(long id)
        {
            return QueryPageRouteDTO(Loader.Where(d => d.PageId == id)).FirstOrDefault();
        }


    }
}
