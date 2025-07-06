using CodeShellCore.CliDispatch.Parsing;
using CodeShellCore.CliDispatch.Routing;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShellCore.Cli.Requests
{
    public class MigrateOldAppHandler : CliRequestHandler<MoldsterAppRequest>
    {
        public MigrateOldAppHandler(IServiceProvider provider) : base(provider)
        {
        }

        public override string FunctionDescription => "Migrate CodeShellCore application from angular 6 to angular 11";

        protected override void Build(ICliRequestBuilder<MoldsterAppRequest> builder)
        {
            builder.Property(e => e.TenantCode, "tenant", "t", isRequired: true);
        }

        protected override Task<Result> HandleAsync(MoldsterAppRequest request,CancellationToken cancellationToken)
        {

            // CliDispatchShell.SetSettingsPath(request.ConfigurationApiPath, request.Environment);

            //CliShell.ConfigurationApiPath = request.ConfigurationApiPath;
            var s = GetService<IMigrationService>();
            s.MigrateBaseModule(request.TenantCode);
            return Task.FromResult(new Result());
        }
    }
}
