using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.Db.Dto
{
    public class DomainWithPagesDTO
    {
        public string DomainName { get; set; }
        public IEnumerable<PageDTO> Pages { get; set; }

        public static Expression<Func<TenantDomain, DomainWithPagesDTO>> Expression
        {
            get
            {
                return x => new DomainWithPagesDTO
                {
                    DomainName = x.Domain.Name,
                    Pages = (from p in x.Pages
                             select new PageDTO
                             {
                                 DomainName = p.TenantDomain.Domain.Name,
                                 TenantCode = p.TenantDomain.Tenant.Code,
                                 TenantId = p.TenantDomain.TenantId,
                                 Page = p,
                                 BaseScript = p.PageCategory.ScriptPath,
                                 ResourceName = p.Resource.Domain.Name + "__" + p.Resource.Name,
                                 ActionName = p.ResourceAction == null ? (p.SpecialPermission != null ? p.SpecialPermission : null) : p.ResourceAction.Name,
                                 PageIdentifier = p.TenantDomain.Domain.Name + "__" + p.Name,
                             }),

                };
            }
        }
    }
}
