using CodeShellCore.Cli;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Pages.Services
{
    public interface IPageHtmlGenerationService 
    {
        void MoveHtmlTemplate(MovePageRequest r);
        void DeleteHtmlTemplate(string tenantCode, string fromPath);
        PageJsonData GenerateComponentTemplate(string moduleName, PageRenderDTO dto);
        void GenerateMainComponentTemplate(string moduleCode);
        void GenerateGuidTemplate(string moduleCode);
    }
}
