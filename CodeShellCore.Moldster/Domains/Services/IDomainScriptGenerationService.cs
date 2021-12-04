using CodeShellCore.Moldster.Pages.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Domains.Services
{
    public interface IDomainScriptGenerationService
    {
        void GenerateDomainModule(string mod, string domain);
        void GenerateDomainModuleById(string moduleCode, long? domId);
        void GenerateRoutes(string module);
        void GenerateModuleDefinitionByPage(PageRenderDTO dto);
    }
}
