using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.CodeGeneration
{
    public interface IScriptGenerationService : IServiceBase
    {
        void GenerateBaseComponent(string templatePath);
        void GenerateComponent(string moduleName, PageRenderDTO dto);
        void GenerateMainComponent(string mod);
        void GenerateModuleDefinition(string module, bool lazy);
        void GenerateGuidComponent(string mod);
        void GenerateBootFile(string moduleCode, bool addStyle = false);
        void GenerateStyle(string moduleCode, string baseName);
        bool GenerateDataService(string resource, string domain = null);
        void GenerateDomainModule(string mod, string domain, bool lazy);
        void GenerateDomainModuleById(string moduleCode, long? domId, bool lazy = true);
        void GenerateRoutes(string module, bool lazy);
        void GeneratePageCategory(long id);
        void GenerateModuleDefinitionByPage(PageRenderDTO dto);
        void MoveScript(MovePageRequest r);
        void DeleteScript(string tenantCode, string fromPath);
    }
}
