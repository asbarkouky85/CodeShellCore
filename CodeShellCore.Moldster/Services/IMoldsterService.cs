using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster.Services
{
    public interface IMoldsterService : IServiceBase
    {
        SubmitResult RenderDomainModule(RenderDTO dto);
        void RenderDomainModule(string mod, string domain, bool lazy);
        void RenderModuleDefinition(string mod);
        void RenderPage(string moduleName, PageRenderDTO dto);
        void ProcessTemplates(string module, string domain = null);
        SubmitResult ProcessForPage(long value);
        SubmitResult RenderAll(string mod);

        SyncResult SyncTenants(long id1, long id2);
    }
}
