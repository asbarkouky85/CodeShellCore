using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster.Pages.Services
{
    public interface IPageControlDataService : IServiceBase
    {
        IEnumerable<DomainWithPagesDTO> GetDomainWithPages(long tenantId, string domainName = null);
        SubmitResult UpdateTemplateControls(PageCategory p, List<ControlRenderDto> controls);
        SubmitResult DeleteUnusedControls(PageCategory p, List<ControlRenderDto> controls);
        SubmitResult UpdateTemplatePages(long id, long? tenantId = null);
    }
}
