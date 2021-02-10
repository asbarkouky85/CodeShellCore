using CodeShellCore.CLI;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Services
{
    public interface IBundlingService : IServiceBase
    {
        void AddStaticFiles();
        void AddSQLFiles();
        void WriteWebpackConfigFiles(bool lazy);
        void GenerateDevWebPackFiles(IEnumerable<string> modules, IEnumerable<string> active = null);
        void GenerateWebPackFiles(string code, IEnumerable<string> others, bool lazy);
        string GetUIVersion();
        void DevelopmentPack();
        void PrepEnvironment(bool prod = false);
        Result ProductionPack(string moduleName, string version = null, bool trace = false);
        bool StartProductionPackIfNeeded(string tenantCode, out BundlingTask tt, string version = null);
        bool IsBundled(string moduleName, string version);
        void AddCodeShell();
        void GenerateEnvironment();
        string GetAppVersion(string code, bool uiIfLarger = false);
        IOutputWriter OutputWriter { get; set; }

        SubmitResult UpdateTenantVersionInDataSource(string code, string version);
    }
}
