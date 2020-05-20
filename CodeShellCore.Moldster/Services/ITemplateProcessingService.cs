using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster.Services
{
    public interface ITemplateProcessingService : IServiceBase
    {
        void GenerateComponentTemplate(string moduleName, PageRenderDTO dto);
        void GenerateMainComponentTemplate(string moduleCode);
        void GenerateGuidTemplate(string moduleCode);
    }
}
