using CodeShellCore.Cli.Routing;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Services;
using System;
using System.Threading.Tasks;

namespace CodeShellCore.Cli.Requests
{
    public class MigrateOldAppHandler : CliRequestHandler<MigrateOldAppRequest>
    {
        public MigrateOldAppHandler(IServiceProvider provider) : base(provider)
        {
        }

        protected override void Build(ICliRequestBuilder<MigrateOldAppRequest> builder)
        {
            builder.FillProperty(e => e.TenantCode, "tenant", 't', isRequired: true);
            builder.FillProperty(e => e.ConfigurationApiPath, "project", 'p', isRequired: true);
            builder.FillProperty(e => e.Environment, "environment", 'e');
        }

        protected override Task<Result> HandleAsync(MigrateOldAppRequest request)
        {

            CliDispatchShell.SetSettingsPath(request.ConfigurationApiPath, request.Environment);

            CliShell.ConfigurationApiPath = request.ConfigurationApiPath;
            var s = GetService<IMigrationService>();
            s.MigrateBaseModule(request.TenantCode);
            return Task.FromResult(new Result());
        }
    }
}
