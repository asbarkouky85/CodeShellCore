using CodeShellCore.Data;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Moldster.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data
{
    public interface IPageCategoryRepository : IRepository<PageCategory>
    {
        IEnumerable<long> GetDomainTemplates(string domain, long tenantId);
        LoadResult<PageCategoryListDTO> GetUnderDomain(long domainId, LoadOptions opt);
    }
}
