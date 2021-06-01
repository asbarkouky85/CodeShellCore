using CodeShellCore.Cli.Requests;
using CodeShellCore.Cli.Routing;
using CodeShellCore.Helpers;
using System;
using System.Threading.Tasks;

namespace CodeShellCore.Cli.Requests.Handlers
{
    public class MigrateOldAppHandler : CliRequestHandler<MigrateOldAppRequest>
    {
        public MigrateOldAppHandler(IServiceProvider provider) : base(provider)
        {
        }

        protected override void Build(ICliRequestBuilder<MigrateOldAppRequest> builder)
        {
            builder.FillProperty(e => e.UIPath, 'u', "uipath", true);
        }

        protected override Task<Result> HandleAsync(MigrateOldAppRequest request)
        {
            return Task.FromResult(new Result());
        }
    }
}
