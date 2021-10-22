using CodeShellCore.Moldster.Pages.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Domains.Services
{
    public interface IDomainScriptGenerationService
    {
        void GenerateAppModule(string module);
        void GenerateDomainModule(string mod, string domain);
        void GenerateDomainModuleById(string moduleCode, long? domId);
        void GenerateMainFile(string moduleCode, bool addStyle = false);
        void GenerateRoutes(string module);
        void GenerateModuleDefinitionByPage(PageRenderDTO dto);
    }
}
