using System.Collections.Generic;
using System.Linq;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Text;
using CodeShellCore.Moldster.PageCategories.Dtos;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Resources;

namespace CodeShellCore.Moldster.Data.Repositories.Internal
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

        public IEnumerable<ModuleCategoryDTO> GetByMoldsterModule(string installPath)
        {
            installPath = installPath.Replace("\\", "/");
            var q = from c in Loader
                    where c.Domain.NameChain.StartsWith("/"+installPath)
                    select new ModuleCategoryDTO
                    {
                        Base = c.BaseComponent,
                        Path = c.ViewPath,
                        Resource = c.ResourceId == null ? null : c.Resource.Name
                    };
            var lst = q.ToList();
            foreach (var l in lst)
            {
                l.Path = l.Path.GetAfterFirst(installPath + "/");
            }
            return lst;
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
                        ResourceId = pc.ResourceId
                    };
            return q.LoadWith(opts);
        }
    }
}
