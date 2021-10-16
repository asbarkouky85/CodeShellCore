using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.Dto
{
    public class TenantPageGuideDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public List<DomainGuidDTO> Domains { get; set; }

        public static Expression<Func<Tenant, TenantPageGuideDTO>> Expression
        {
            get
            {
                return e => new TenantPageGuideDTO
                {
                    Code = e.Code,
                    Name = e.Name,
                    Domains = e.Pages.Select(d => new DomainGuidDTO
                    {
                        Name = d.Domain.Name,
                        Resources=new List<ResourceGuidDTO>()
                        //TODO:
                        //Resources = (from r in d.Domain.Resources
                        //             where r.Pages.Any(p => p.TenantDomainId == d.Id)
                        //             select new ResourceGuidDTO
                        //             {
                        //                 Name = r.Name,
                        //                 PageCount = r.Pages.Count(s => s.HasRoute && s.TenantDomainId == d.Id),
                        //                 Pages = (from p in r.Pages
                        //                          where p.HasRoute && p.TenantDomainId == d.Id
                        //                          select new PageGuidDTO
                        //                          {
                        //                              Name = p.TenantDomain.Domain.Name + "__" + p.Name,
                        //                              PrivilegeName = p.ResourceActionId == null ? p.PrivilegeType : p.ResourceAction.Name,
                        //                              AppsString = p.Apps
                        //                          }).ToList()
                        //             }).ToList()
                    }).ToList()
                };
            }
        }
    }

    public class DomainGuidDTO
    {
        public string Name { get; set; }

        public List<ResourceGuidDTO> Resources { get; set; }
    }

    public class ResourceGuidDTO
    {
        public int PageCount { get; set; }
        public string Name { get; set; }

        public List<PageGuidDTO> Pages { get; set; }

        public IEnumerable<PageGuidDTO> ViewPages { get; set; }
        public IEnumerable<PageGuidDTO> DetailsPages { get; set; }
        public IEnumerable<PageGuidDTO> UpdatePages { get; set; }
        public IEnumerable<PageGuidDTO> InsertPages { get; set; }
        public Dictionary<string, List<PageGuidDTO>> OtherPages { get; set; }
    }

    public class PageGuidDTO
    {
        public string Name { get; set; }
        public string PrivilegeName { get; set; }
        public string AppsString { get; set; }
        public IEnumerable<string> Apps { get { return AppsString.Split(','); } }
    }
}
