using CodeShellCore.Cli;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Builder
{
    public interface IBundlingService : IServiceBase
    {
        void AddStaticFiles(bool replace);
        void AddCodeShell(bool replace);
        void AddShellComponents(bool replace);
        void GenerateEnvironment(bool replace);
        void AddSQLFiles();
        string GetUIVersion();

        void PrepEnvironment(bool prod = false);
        Result ProductionPack(string moduleName, string version = null, bool trace = false);
        bool StartProductionPackIfNeeded(string tenantCode, out BundlingTask tt, string version = null);
        bool IsBundled(string moduleName, string version);
        
        
        string GetAppVersion(string code, bool uiIfLarger = false);
        IOutputWriter OutputWriter { get; set; }

        SubmitResult UpdateTenantVersionInDataSource(string code, string version);
    }
}
