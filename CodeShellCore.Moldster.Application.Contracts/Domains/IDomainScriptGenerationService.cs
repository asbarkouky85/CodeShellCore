using CodeShellCore.Moldster.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Domains
{
    public interface IDomainScriptGenerationService
    {
        Task GenerateDomainModule(string mod, string domain);
        Task GenerateDomainModuleById(string moduleCode, long? domId);
        Task GenerateRoutes(string module);
        Task GenerateModuleDefinitionByPage(PageRenderDTO dto);
    }
}
