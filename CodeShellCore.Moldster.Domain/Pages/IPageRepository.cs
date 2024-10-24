using CodeShellCore.Data;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Pages.Views;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageRepository : IRepository<Page>
    {
        IEnumerable<Page> GetReferencing(long pageId, long tenantId);
        IEnumerable<T> GetDomainPagesForRouting<T>(string tenantCode, long domainId, bool chldren = false);
        List<PageIdentifierView> GetDistinctIdentifiers();
        PagedResult<T> FindUsing<T>(FindPageRequest request, PagedListRequest opts) where T : class;
        PagedResult<T> GetUnderDomain<T>(long domainId, PagedListRequest opt) where T : class;
        Page GetForCustomization(long id);
        PageAndType FindLinkedPage(string paramName, string val, long tenantId, ref List<string> add);
        PageAndType FindLinkedPageByName(string paramName, string val, long tenantId, ref List<string> add);

        string GetHomePagePath(string modCode);
        void FillReferencedBy(IEnumerable<IPageReferenceCounter> listT);
        void FillReferences(IEnumerable<IPageReferenceCounter> listT);
        void UpdatePageViewParamsJson(Page p, PageParameterForJson[] ps, PageRouteView r, FieldDefinition[] def);
        Task<List<PageOptions>> GetPageOptionsByCategory(long categoryId, long tenantId);
    }
}
