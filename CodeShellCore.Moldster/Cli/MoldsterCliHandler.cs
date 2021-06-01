using CodeShellCore.Cli.Routing;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Cli
{
    public class MoldsterCliHandler : StartupCliRequestHandler<MoldsterCliOptions>
    {
        public MoldsterCliHandler()
        {
        }

        protected override void Build(ICliRequestBuilder<MoldsterCliOptions> builder)
        {
            builder.FillProperty(e => e.ConfigurationApiPath, 'p', "project");
        }

        protected override Task<Result> HandleAsync(MoldsterCliOptions request)
        {
            CliDispatchShell.ConfigurationApiPath = request.ConfigurationApiPath;
            return Task.FromResult(new Result());
        }
    }
}
