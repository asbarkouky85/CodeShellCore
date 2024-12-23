using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Services;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageParameterDataService : IServiceBase
    {
        Task<SubmitResult> UpdateTemplatePages(long id, long tenantId);
        Task<SubmitResult> UpdateTemplatePagesViewParamsJson(long tenantId, long? categoryId = null);
        Task<SubmitResult> UpdateTemplatePagesViewParamsJson(string tenantCode);
        Task<PagedResult<PageReferenceDTO>> GetReferences(ParameterRequest req, PagedListRequestDto opt);
    }
}
