using CodeShellCore.Data;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Data.Repositories
{
    public interface IPageCategoryRepository : IRepository<PageCategory>
    {
        IEnumerable<long> GetDomainTemplates(string domain, long tenantId);
        LoadResult<PageCategoryListDTO> GetUnderDomain(long domainId, LoadOptions opt);
        void Add(PageCategory cat, Domain dom, Resource res = null);
        IEnumerable<ModuleCategoryDTO> GetByMoldsterModule(string installPath);
    }
}
