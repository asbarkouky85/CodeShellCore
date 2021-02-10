using System.Collections.Generic;
using System.Linq;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Db.Dto;

namespace CodeShellCore.Moldster.Db.Data.Internal
{
    public class PageCategoryRepository : MoldsterRepository<PageCategory, MoldsterContext>, IPageCategoryRepository
    {
        public PageCategoryRepository(MoldsterContext con) : base(con)
        {
        }

        public IEnumerable<long> GetDomainTemplates(string domain, long tenantId)
        {
            if (string.IsNullOrEmpty(domain))
                return new long[0];
            var query = domain;
            query = query[0] != '/' ? "/" + query : query;
            query = query[query.Length - 1] != '/' ? query + "/" : query;
            return Loader
                .Where(d => d.Domain.NameChain.Contains(query))
                .Select(d => d.Id).ToList();
        }

        public LoadResult<PageCategoryListDTO> GetUnderDomain(long domainId, LoadOptions opt)
        {
            var opts = opt.GetOptionsFor<PageCategoryListDTO>();

            var q = from pc in DbContext.PageCategories
                    where pc.Domain.Chain.Contains("|" + domainId.ToString() + "|")
                    select new PageCategoryListDTO
                    {
                        Id = pc.Id,
                        Name = pc.Name,
                        BaseComponent = pc.BaseComponent,
                        ResourceName = pc.Resource.Name,
                        DomainId = pc.DomainId,
                        ViewPath = pc.ViewPath,
                        Layout = pc.Layout,
                        DomainName = pc.Domain.Name,
                        ResourceId = pc.ResourceId,
                        ScriptPath = pc.ScriptPath
                    };
            return q.LoadWith(opts);
        }
    }
}
