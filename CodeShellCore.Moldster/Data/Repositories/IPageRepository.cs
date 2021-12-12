using CodeShellCore.Data;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Razor;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CodeShellCore.Moldster.Pages.Dtos;
using CodeShellCore.Moldster.Pages;

namespace CodeShellCore.Moldster.Data.Repositories
{
    public interface IPageRepository : IRepository<Page>
    {
        IEnumerable<PageDTO> GetDomainPagesForRouting(string tenantCode, long domainId, bool searchChildren = false);
        string GetHomePagePath(string modCode);
        LoadResult<PageListDTO> GetUnderDomain(long domainId, LoadOptions opt);
        PageAndTypeDTO FindLinkedPage(string paramName, string val, long tenantId, ref List<string> add);
        PageAndTypeDTO FindLinkedPageByName(string paramName, string val, long tenantId, ref List<string> add);
        LoadResult<PageListDTO> FindUsing(FindPageRequest request, LoadOptions opts);
        void UpdatePageViewParamsJson(Page p, PageParameterForJson[] ps, PageRouteDTO r, FieldDefinition[] def);
        IEnumerable<Page> GetReferencing(long pageId, long tenantId);
        void FillReferencedBy(IEnumerable<PageListDTO> listT);
        void FillReferences(IEnumerable<PageListDTO> listT);
        PageDTO FindSingleForRendering(Expression<Func<Page, bool>> p);
        List<PageIdentifierDTO> GetDistinctIdentifiers();
    }
}
