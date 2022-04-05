using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageScriptGenerationService
    {
        void GenerateAppComponent(string mod);
        void GenerateComponent(string moduleName, PageRenderDTO dto, PageJsonData data);
        void MoveScript(MovePageRequest r);
        void DeleteScript(string tenantCode, string fromPath);
    }
}
