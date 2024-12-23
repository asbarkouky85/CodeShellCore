using CodeShellCore.Data.Helpers;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageControlDataService : IServiceBase
    {
        Task<IEnumerable<DomainWithPagesDTO>> GetDomainWithPages(long tenantId, string domainName = null);
        Task<SubmitResult> UpdateTemplateControls(PageCategory p, List<ControlRenderDto> controls);
        Task<SubmitResult> DeleteUnusedControls(PageCategory p, List<ControlRenderDto> controls);
        Task<SubmitResult> UpdateTemplatePages(long id, long? tenantId = null);
    }
}
