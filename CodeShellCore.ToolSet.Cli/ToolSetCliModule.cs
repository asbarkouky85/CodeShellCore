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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet
{
    [DependsOn(typeof(ToolsApplicationModule))]
    public class ToolSetCliModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            var builder = context.Services.GetCliRouteBuilder();
            builder.AddHandler<AbpSyncLanguagesRequestHandler>("sync-loc-abp");
            builder.AddHandler<NugetPublishRequestHandler>("upload-nuget");
            builder.AddHandler<SqlQueryRequestHandler>("sql-exec");
            builder.AddHandler<SqlRestoreRequestHandler>("sql-restore");
            builder.AddHandler<ProjectVersionRequestHandler>("set-version");
            builder.AddHandler<ZipRequestHandler>("zip");
            builder.AddHandler<ZipRequestHandler>("extract", new { Extract = "true" });
            builder.AddHandler<CopyRequestHandler>("copy");
            builder.AddHandler<TsProxyRequestHandler>("gen-proxy");
            builder.AddHandler<GenerateModuleClassesRequestHandler>("gen-modules");
            builder.AddHandler<HelpRequestHandler>("help");
            builder.AddHandler<ReplaceParametersRequestHandler>("replace");
            builder.AddHandler<DownloadHandler>("download");
        }
    }
}
