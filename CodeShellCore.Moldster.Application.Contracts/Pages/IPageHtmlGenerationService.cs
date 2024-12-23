using CodeShellCore.Cli;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageHtmlGenerationService
    {
        Task<PageJsonData> GenerateComponentTemplate(string moduleName, PageRenderDTO dto);
        Task MoveHtmlTemplate(MovePageRequest r);
        Task DeleteHtmlTemplate(string tenantCode, string fromPath);
        Task GenerateMainComponentTemplate(string moduleCode);
    }
}
