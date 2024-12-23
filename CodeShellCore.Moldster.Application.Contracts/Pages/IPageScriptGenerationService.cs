using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageScriptGenerationService
    {
        Task GenerateAppComponent(string mod);
        Task GenerateComponent(string moduleName, PageRenderDTO dto, PageJsonData data);
        Task MoveScript(MovePageRequest r);
        Task DeleteScript(string tenantCode, string fromPath);
    }
}
