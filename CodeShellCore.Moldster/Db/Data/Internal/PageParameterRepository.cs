using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeShellCore.Moldster.Db.Dto;

namespace CodeShellCore.Moldster.Db.Data.Internal
{
    public class PageParameterRepository : MoldsterRepository<PageParameter, MoldsterContext>, IPageParameterRepository
    {
        public PageParameterRepository(MoldsterContext con) : base(con)
        {
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
                       pages.Where(e => e.Id == d.LinkedPageId).Select(e => e.ViewPath).FirstOrDefault() : null)
                   };
        }

        public IEnumerable<PageParameterForJson> FindForJsonByPage(long pageId)
        {
            var q = Loader.Where(d => d.PageId == pageId);
            return QueryPageParameterForJson(q).ToList();
        }

        public IEnumerable<PageParameterForJson> FindForJson(long tenantId, long? pageCategoryId = null)
        {
            var q = Loader.Where(d => (d.Page.PageCategoryId == pageCategoryId || pageCategoryId == null) && d.Page.TenantId == tenantId);
            return QueryPageParameterForJson(q).ToList();
        }

        public List<PageParameterDTO> FindForPage(long id)
        {
            var catId = DbContext.Pages.Where(d => d.Id == id).Select(d => d.PageCategoryId).FirstOrDefault();

            var q = from s in DbContext.PageCategoryParameters
                    where s.PageCategoryId == catId
                    orderby s.Type
                    select new PageParameterDTO
                    {
                        Id = s.Id,
                        Entity = s.PageParameters.Where(d => d.PageId == id).FirstOrDefault() ?? new PageParameter { UseDefault = true },
                        Name = s.Name,
                        Type = s.Type,
                        DefaultValue = s.DefaultValue,
                        ViewPath = (from e in s.PageParameters
                                    where e.PageId == id
                                    select e.LinkedPageId.HasValue ? DbContext.Pages
                                        .Where(p => p.Id == e.LinkedPageId)
                                        .Select(p => p.ViewPath)
                                        .FirstOrDefault() : null)
                                 .FirstOrDefault()
                    };
            return q.ToList();
        }
    }
}
