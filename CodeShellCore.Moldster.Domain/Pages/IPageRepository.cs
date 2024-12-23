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
        Task<IEnumerable<Page>> GetReferencing(long pageId, long tenantId);
        Task<IEnumerable<T>> GetDomainPagesForRouting<T>(string tenantCode, long domainId, bool chldren = false);
        Task<List<PageIdentifierView>> GetDistinctIdentifiers();
        Task<PagedResult<T>> FindUsing<T>(FindPageRequest request, PagedListRequest opts) where T : class;
        Task<PagedResult<T>> GetUnderDomain<T>(long domainId, PagedListRequest opt) where T : class;
        Task<Page> GetForCustomization(long id);
        Task<PageAndType> FindLinkedPage(string paramName, string val, long tenantId, ref List<string> add);
        Task<PageAndType> FindLinkedPageByName(string paramName, string val, long tenantId, ref List<string> add);

        Task<string> GetHomePagePath(string modCode);
        Task FillReferencedBy(IEnumerable<IPageReferenceCounter> listT);
        Task FillReferences(IEnumerable<IPageReferenceCounter> listT);
        Task UpdatePageViewParamsJson(Page p, PageParameterForJson[] ps, PageRouteView r, FieldDefinition[] def);
        Task<List<PageOptions>> GetPageOptionsByCategory(long categoryId, long tenantId);
    }
}
