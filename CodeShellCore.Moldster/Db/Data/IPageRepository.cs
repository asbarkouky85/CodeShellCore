using CodeShellCore.Data;
using CodeShellCore.Moldster.Db.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data
{
    public interface IPageRepository : IRepository<Page>
    {
        IEnumerable<PageDTO> GetSharedPagesForRouting(string tenantCode);
        IEnumerable<PageDTO> GetDomainPagesForRouting(string tenantCode, string domain);
    }
}
