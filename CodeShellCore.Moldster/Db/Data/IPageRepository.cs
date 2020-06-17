using CodeShellCore.Data;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Moldster.Db.Razor;
using System;
using System.Collections.Generic;

using System.Text;

namespace CodeShellCore.Moldster.Db.Data
{
    public interface IPageRepository : IRepository<Page>
    {
        IEnumerable<PageDTO> GetSharedPagesForRouting(string tenantCode);
        IEnumerable<PageDTO> GetDomainPagesForRouting(string tenantCode, long domainId);
        string GetHomePagePath(string modCode);
        LoadResult<PageListDTO> GetUnderDomain(long domainId, LoadOptions opt);
        PageAndTypeDTO FindLinkedPage(string paramName, string val, long tenantId, ref List<string> add);
        PageAndTypeDTO FindLinkedPageByName(string paramName, string val, long tenantId, ref List<string> add);
        LoadResult<PageListDTO> FindUsing(FindPageRequest request, LoadOptions opts);
        void UpdatePageViewParamsJson(Page p, PageParameterForJson[] ps, PageRouteDTO r, FieldDefinition[] def);
        IEnumerable<Page> GetReferencing(long pageId, long tenantId);
        void FillReferencedBy(IEnumerable<PageListDTO> listT);
        void FillReferences(IEnumerable<PageListDTO> listT);
    }
}
