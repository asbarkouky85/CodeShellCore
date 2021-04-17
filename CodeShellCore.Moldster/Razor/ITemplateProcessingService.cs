using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster.Razor
{
    public interface ITemplateProcessingService : IServiceBase
    {
        bool CollectTemplateData(long id);
        void ProcessForTenant(string templatePath, string modCode);
        bool ProcessForTenant(long id, long tenantId);
        void UpdateTemplatePages(long id, long tenantId);
        void MoveHtmlTemplate(MovePageRequest r);
        void DeleteHtmlTemplate(string tenantCode, string fromPath);
        PageJsonData GenerateComponentTemplate(string moduleName, PageRenderDTO dto);
        void GenerateMainComponentTemplate(string moduleCode);
        void GenerateGuidTemplate(string moduleCode);
    }
}
