using CodeShellCore.Moldster.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CodeShellCore.Moldster.Tenants
{
    public class TenantPageGuideDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public List<DomainGuidDTO> Domains { get; set; }

        //public static Expression<Func<Tenant, TenantPageGuideDTO>> Expression
        //{
        //    get
        //    {
        //        return e => new TenantPageGuideDTO
        //        {
        //            Code = e.Code,
        //            Name = e.Name,
        //            Domains = e.Pages.Select(d => new DomainGuidDTO
        //            {
        //                Name = d.Domain.Name,
        //                Resources = new List<ResourceGuidDTO>()
        //                //TODO:
        //                //Resources = (from r in d.Domain.Resources
        //                //             where r.Pages.Any(p => p.TenantDomainId == d.Id)
        //                //             select new ResourceGuidDTO
        //                //             {
        //                //                 Name = r.Name,
        //                //                 PageCount = r.Pages.Count(s => s.HasRoute && s.TenantDomainId == d.Id),
        //                //                 Pages = (from p in r.Pages
        //                //                          where p.HasRoute && p.TenantDomainId == d.Id
        //                //                          select new PageGuidDTO
        //                //                          {
        //                //                              Name = p.TenantDomain.Domain.Name + "__" + p.Name,
        //                //                              PrivilegeName = p.ResourceActionId == null ? p.PrivilegeType : p.ResourceAction.Name,
        //                //                              AppsString = p.Apps
        //                //                          }).ToList()
        //                //             }).ToList()
        //            }).ToList()
        //        };
        //    }
        //}
    }
}
