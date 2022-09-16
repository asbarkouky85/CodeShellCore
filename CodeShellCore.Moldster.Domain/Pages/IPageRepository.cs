using CodeShellCore.Data;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Pages.Views;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageRepository : IRepository<Page>
    {
        IEnumerable<Page> GetReferencing(long pageId, long tenantId);
        IEnumerable<T> GetDomainPagesForRouting<T>(string tenantCode, long domainId, bool chldren = false);
        List<PageIdentifierView> GetDistinctIdentifiers();
        LoadResult<T> FindUsing<T>(FindPageRequest request, LoadOptions opts) where T : class;
        LoadResult<T> GetUnderDomain<T>(long domainId, LoadOptions opt) where T : class;
        Page GetForCustomization(long id);
        PageAndType FindLinkedPage(string paramName, string val, long tenantId, ref List<string> add);
        PageAndType FindLinkedPageByName(string paramName, string val, long tenantId, ref List<string> add);

        string GetHomePagePath(string modCode);
        void FillReferencedBy(IEnumerable<IPageReferenceCounter> listT);
        void FillReferences(IEnumerable<IPageReferenceCounter> listT);
        void UpdatePageViewParamsJson(Page p, PageParameterForJson[] ps, PageRouteView r, FieldDefinition[] def);
    }
}
