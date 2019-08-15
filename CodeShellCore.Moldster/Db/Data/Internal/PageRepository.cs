using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Moldster.Db.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data.Internal
{
    public class PageRepository : Repository_Int64<Page, MoldsterContext>, IPageRepository
    {
        public PageRepository(MoldsterContext con) : base(con)
        {
        }

        public IEnumerable<PageDTO> GetDomainPagesForRouting(string tenantCode, long domainId)
        {
            return FindAs(PageDTO.ExpressionForRouting,
                d => d.Tenant.Code == tenantCode &&
                d.DomainId == domainId &&
                d.HasRoute);
        }

        public IEnumerable<PageDTO> GetSharedPagesForRouting(string tenantCode)
        {
            return FindAs(PageDTO.ExpressionForRouting, d => d.Tenant.Code == tenantCode && d.CanEmbed && !d.HasRoute);
        }
    }
}
