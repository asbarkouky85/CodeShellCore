using CodeShellCore.CliDispatch.Parsing;
using CodeShellCore.CliDispatch.Routing;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Services;
using System;
using System.Threading.Tasks;

namespace CodeShellCore.Cli.Requests
{
    public class RestructureAppHandler : CliRequestHandler<MoldsterAppRequest>
    {
        public RestructureAppHandler(IServiceProvider provider) : base(provider)
        {
        }

        public override string FunctionDescription => "Restructure Code";

        protected override void Build(ICliRequestBuilder<MoldsterAppRequest> builder)
        {
            builder.Property(e => e.TenantCode, "tenant", "t", isRequired: true);
        }

        protected override async Task<Result> HandleAsync(MoldsterAppRequest request)
        {
            var s = GetService<IMigrationService>();
            return await s.CategoriesToComponents(request.TenantCode);
        }
    }
}
