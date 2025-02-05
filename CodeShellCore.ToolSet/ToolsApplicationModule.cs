using CodeShellCore.CliDispatch;
using CodeShellCore.Modularity;
using CodeShellCore.ToolSet.Download;
using CodeShellCore.ToolSet.Ftp;
using CodeShellCore.ToolSet.Help;
using CodeShellCore.ToolSet.Localization;
using CodeShellCore.ToolSet.Modularity;
using CodeShellCore.ToolSet.Nuget;
using CodeShellCore.ToolSet.Replace;
using CodeShellCore.ToolSet.Sql;
using CodeShellCore.ToolSet.TsProxy;
using CodeShellCore.ToolSet.Versions;
using CodeShellCore.ToolSet.Zip;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.ToolSet
{
    [DependsOn(
        typeof(CodeShellCliDispatchModule)
        )]
    public class ToolsApplicationModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddTransient<IModuleClassGenerationService, ModuleClassGenerationService>();

            var builder = context.Services.GetCliRouteBuilder();
            builder.AddHandler<AbpSyncLanguagesRequestHandler>("sync-loc-abp");
            builder.AddHandler<SyncLanguageRequestHandler>("sync-loc");
            builder.AddHandler<CopyRequestHandler>("copy");
            builder.AddHandler<DownloadHandler>("download");
            builder.AddHandler<GenerateModuleClassesRequestHandler>("gen-modules");
            builder.AddHandler<HelpRequestHandler>("help");
            builder.AddHandler<NugetPublishRequestHandler>("upload-nuget");
            builder.AddHandler<ProjectVersionRequestHandler>("set-version");
            builder.AddHandler<ReplaceParametersRequestHandler>("replace");
            builder.AddHandler<SqlQueryRequestHandler>("sql-exec");
            builder.AddHandler<SqlRestoreRequestHandler>("sql-restore");
            builder.AddHandler<TsProxyRequestHandler>("gen-proxy");
            builder.AddHandler<ZipRequestHandler>("extract", new { Extract = "true" });
            builder.AddHandler<ZipRequestHandler>("zip");
        }
    }
}
