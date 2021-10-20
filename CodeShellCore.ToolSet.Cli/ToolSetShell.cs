using CodeShellCore.Cli.Routing;
using CodeShellCore.ToolSet.Ftp;
using CodeShellCore.ToolSet.Localization;
using CodeShellCore.ToolSet.Nuget;
using CodeShellCore.ToolSet.Sql;
using CodeShellCore.ToolSet.Versions;
using CodeShellCore.ToolSet.Zip;

namespace CodeShellCore.ToolSet
{
    public class ToolSetShell : CliDispatchShell
    {
        public ToolSetShell(string[] args) : base(args)
        {
        }

        protected override void RegisterHandlers(ICliDispatcherBuilder builder)
        {
            builder.AddHandler<AbpSyncLanguagesRequestHandler>("sync-loc-abp");
            builder.AddHandler<NugetPublishRequestHandler>("upload-nuget");
            builder.AddHandler<SqlQueryRequestHandler>("sql-exec");
            builder.AddHandler<SqlRestoreRequestHandler>("sql-restore");
            builder.AddHandler<ProjectVersionRequestHandler>("set-version");
            builder.AddHandler<ZipRequestHandler>("zip");
            builder.AddHandler<CopyRequestHandler>("copy");

        }
    }
}
