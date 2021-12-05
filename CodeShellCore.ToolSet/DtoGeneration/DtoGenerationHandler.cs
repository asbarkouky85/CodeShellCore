using CodeShellCore.Cli.Routing;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Cli.DtoGeneration
{
    public class DtoGenerationHandler : CliRequestHandler<DtoGenerationRequest>
    {
        public DtoGenerationHandler(IServiceProvider provider) : base(provider)
        {
        }

        protected override void Build(ICliRequestBuilder<DtoGenerationRequest> builder)
        {
            builder.FillProperty(e => e.WorkingDirectory, "folder", 'd', 1, true);
        }

        protected override Task<Result> HandleAsync(DtoGenerationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
