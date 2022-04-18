using CodeShellCore.Cli;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster.Builder.Services
{
    public interface IBundlingService : IServiceBase
    {

        string GetUIVersion();

        void PrepEnvironment(bool prod = false);
        Result ProductionPack(string moduleName, string version = null, bool trace = false);
        bool StartProductionPackIfNeeded(string tenantCode, out BundlingTask tt, string version = null);
        bool IsBundled(string moduleName, string version);
        string GetAppVersion(string code, bool uiIfLarger = false);
        string CompressModuleBundle(string tenant, string version);
        IOutputWriter OutputWriter { get; set; }

        SubmitResult UpdateTenantVersionInDataSource(string code, string version);
    }
}
