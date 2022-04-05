using CodeShellCore.Data;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageRepository : IRepository<Page>
    {
        IEnumerable<PageDetailsDto> GetDomainPagesForRouting(string tenantCode, long domainId, bool searchChildren = false);
        string GetHomePagePath(string modCode);
        LoadResult<PageListDTO> GetUnderDomain(long domainId, LoadOptions opt);
        PageAndTypeDTO FindLinkedPage(string paramName, string val, long tenantId, ref List<string> add);
        PageAndTypeDTO FindLinkedPageByName(string paramName, string val, long tenantId, ref List<string> add);
        LoadResult<PageListDTO> FindUsing(FindPageRequest request, LoadOptions opts);
        void UpdatePageViewParamsJson(Page p, PageParameterForJson[] ps, PageRouteDTO r, FieldDefinition[] def);
        IEnumerable<Page> GetReferencing(long pageId, long tenantId);
        void FillReferencedBy(IEnumerable<PageListDTO> listT);
        void FillReferences(IEnumerable<PageListDTO> listT);
        PageDetailsDto FindSingleForRendering(Expression<Func<Page, bool>> p);
        List<PageIdentifierDTO> GetDistinctIdentifiers();
        Page GetForCustomization(long id);
    }
}
