using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.CodeGeneration
{
    public interface IScriptGenerationService : IServiceBase
    {
        void GenerateBaseComponent(string templatePath);
        void GenerateComponent(string moduleName, PageRenderDTO dto, PageJsonData data);
        bool GenerateHttpService(string resource, string domain = null);

        void GenerateAppComponent(string mod);
        void GenerateAppModule(string module);
        void GenerateDomainModule(string mod, string domain);
        void GenerateDomainModuleById(string moduleCode, long? domId);

        void GenerateMainFile(string moduleCode, bool addStyle = false);


        void GenerateRoutes(string module);
        void GeneratePageCategory(long id);
        void GenerateModuleDefinitionByPage(PageRenderDTO dto);

        void MoveScript(MovePageRequest r);
        void DeleteScript(string tenantCode, string fromPath);
    }
}
