using CodeShellCore.Moldster.Pages.Dtos;
using CodeShellCore.Moldster.Razor;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Pages.Services
{
    public interface IPageScriptGenerationService
    {
        void GenerateAppComponent(string mod);
        void GenerateComponent(string moduleName, PageRenderDTO dto, PageJsonData data);
        void MoveScript(MovePageRequest r);
        void DeleteScript(string tenantCode, string fromPath);
    }
}
