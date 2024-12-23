using CodeShellCore.Data.Helpers;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Sql;
using CodeShellCore.Services;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster
{
    public interface IMoldsterService : IServiceBase
    {
        Task<SubmitResult> RenderDomainModule(RenderDTO dto);
        Task RenderDomainModule(string mod, string domain, bool lazy);
        Task RenderModuleDefinition(string mod);
        Task RenderPage(string moduleName, PageRenderDTO dto);
        Task ProcessTemplates(string module, string domain = null);
        Task<SubmitResult> ProcessForPage(long value);
        Task<SubmitResult> RenderAll(string mod);

        Task<SyncResult> SyncTenants(long id1, long id2);
    }
}
