using CodeShellCore.Moldster.Pages.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.Dto
{
    public class DomainWithPagesDTO
    {

        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string DomainName { get; set; }
        public IEnumerable<PageDTO> Pages { get; set; }
        public IEnumerable<DomainWithPagesDTO> SubDomains { get; set; }

        

        public static Expression<Func<Domain, DomainWithPagesDTO>> RenderingExpression
        {
            get
            {
                return x => new DomainWithPagesDTO
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    DomainName = x.Name,
                    Pages = (from e in x.Pages
                             select new PageDTO
                             {
                                 TenantCode = e.Tenant.Code,
                                 TenantId = e.TenantId,
                                 Page = e,
                                 //BaseScript = e.PageCategory.ScriptPath,
                                 ParentHasResource = e.PageCategory.ResourceId != null,
                                 ResourceName = e.Resource.Name,
                                 DomainName = e.Domain.Name,
                                 CollectionId = e.SourceCollection == null ? null : e.SourceCollection.Name
                             }).ToList()
                };
            }
        }

        //public string LazyLoadingRoute
        //{
        //    //() => import('./dashboard/dashboard.module').then(m => m.DashboardModule)
        //    get
        //    {
        //        return "{ path:\"" + DomainName + "\", loadChildren:\"./" + DomainName + "/" + DomainName + "Module#" + DomainName + "Module\" },\n\t";
        //    }
        //}

        public void AppendChildren(IEnumerable<DomainWithPagesDTO> lst)
        {
            SubDomains = lst.Where(d => d.ParentId == Id);
            foreach (var s in SubDomains)
            {
                s.AppendChildren(lst);
            }
        }
    }
}
