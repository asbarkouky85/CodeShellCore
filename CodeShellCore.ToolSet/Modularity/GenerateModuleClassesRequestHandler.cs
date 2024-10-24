using CodeShellCore.CliDispatch.Parsing;
using CodeShellCore.CliDispatch.Routing;
using CodeShellCore.Helpers;
using CodeShellCore.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Modularity
{
    public class GenerateModuleClassesRequestHandler : CliRequestHandler<GenerateModuleClassesRequest>
    {
        public GenerateModuleClassesRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        public override string FunctionDescription => "Generate Module Classes";

        protected override void Build(ICliRequestBuilder<GenerateModuleClassesRequest> builder)
        {
            builder.Property(e => e.SolutionFolder, "folder", "d", 1, true);
        }

        protected override async Task<Result> HandleAsync(GenerateModuleClassesRequest request)
        {
            return await GetService<IModuleClassGenerationService>().Generate(request);
        }
    }
}
