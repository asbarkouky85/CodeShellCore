using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Moldster.Db.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data.Internal
{
    public class PageRepository : Repository_Int64<Page, ConfigurationContext>,IPageRepository
    {
        public PageRepository(ConfigurationContext con) : base(con)
        {
        }

        public IEnumerable<PageDTO> GetDomainPagesForRouting(string tenantCode, string domain)
        {
            return FindAs(PageDTO.ExpressionForRouting, 
                d => d.TenantDomain.Tenant.Code == tenantCode && 
                d.TenantDomain.Domain.Name == domain && 
                d.HasRoute);
        }

        public IEnumerable<PageDTO> GetSharedPagesForRouting(string tenantCode)
        {
            return FindAs(PageDTO.ExpressionForRouting, d => d.TenantDomain.Tenant.Code == tenantCode && d.CanEmbed && !d.HasRoute);
        }
    }
}
