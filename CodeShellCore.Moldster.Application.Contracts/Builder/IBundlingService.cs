using CodeShellCore.Cli;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Services;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Builder
{
    public interface IBundlingService : IServiceBase
    {

        Task<string> GetUIVersion();

        Task PrepEnvironment(bool prod = false);
        Task<Result> ProductionPack(string moduleName, string version = null, bool trace = false);
        bool StartProductionPackIfNeeded(string tenantCode, out BundlingTask tt, string version = null);
        Task<bool> IsBundled(string moduleName, string version);
        Task<string> GetAppVersion(string code, bool uiIfLarger = false);
        Task<string> CompressModuleBundle(string tenant, string version);
        IOutputWriter OutputWriter { get; set; }

        Task<SubmitResult> UpdateTenantVersionInDataSource(string code, string version);
    }
}
