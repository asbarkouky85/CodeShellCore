using CodeShellCore.Services;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Services
{
    public interface IScriptGenerationService : IServiceBase
    {
        void GenerateBaseComponent(string templatePath);
        void GenerateComponent(string moduleName, string domain, string viewPath);
        void GenerateMainComponent(string mod);
        void GenerateModuleDefinition(string module, bool lazy);
        void GenerateGuidComponent(string mod);
        void GenerateBootFile(string moduleCode);
        bool GenerateDataService(string resource, string domain = null);
        void GenerateDomainModule(string moduleCode, string dom, bool lazy = true);
        void GenerateDevWebPackFiles(IEnumerable<string> modules, IEnumerable<string> active = null);
        void GenerateWebPackFiles(string code, IEnumerable<string> others, bool lazy);
        void GenerateRoutes(string module, bool lazy);
        void GenerateEnums();
        string MapEntity<T>();
        string MapEnum<T>();
        void GenerateEnvironment();
    }
}
