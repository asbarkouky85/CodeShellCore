using System.Collections.Generic;
using System.Linq;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Text;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Resources;

namespace CodeShellCore.Moldster.PageCategories
{
    public class PageCategoryRepository : MoldsterRepository<PageCategory, MoldsterContext>, IPageCategoryRepository
    {

        public PageCategoryRepository(MoldsterContext con) : base(con)
        {

        }

        public void Add(PageCategory item, Domain dom, Resource res = null)
        {
            if (string.IsNullOrEmpty(item.Name))
                item.Name = item.ViewPath?.GetAfterLast("/");
            if (item.Id == 0)
                item.Id = Utils.GenerateID();
            dom.PageCategories.Add(item);
            item.DomainId = dom.Id;
            if (res != null)
            {
                item.ResourceId = res.Id;
                res.PageCategories.Add(item);
            }
            Add(item);
        }

        public IEnumerable<T> GetByMoldsterModule<T>(string installPath)
        {
            installPath = installPath.Replace("\\", "/");
            var q = from c in Loader
                    where c.Domain.NameChain.StartsWith("/" + installPath)
                    select c;

            return QueryDto<T>(q).ToList();
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

        public LoadResult<T> GetUnderDomain<T>(long domainId, LoadOptions opt) where T : class
        {
            var opts = opt.GetOptionsFor<T>();

            var q = from pc in DbContext.PageCategories
                    where pc.Domain.Chain.Contains("|" + domainId.ToString() + "|")
                    select pc;

            return QueryDto<T>(q).LoadWith(opts);
        }
    }
}
