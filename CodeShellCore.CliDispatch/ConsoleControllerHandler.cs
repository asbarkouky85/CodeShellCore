using CodeShellCore.Cli;
using CodeShellCore.CliDispatch.Parsing;
using CodeShellCore.CliDispatch.Routing;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShellCore.CliDispatch
{
    public class ConsoleControllerHandler<TController> : CliRequestHandler<object> where TController : ConsoleController
    {
        public ConsoleControllerHandler(IServiceProvider provider) : base(provider)
        {
        }

        public override string FunctionDescription => "Routing using numbers";

        protected override void Build(ICliRequestBuilder<object> builder)
        {

        }

        protected override async Task<Result> HandleAsync(object request,CancellationToken token)
        {
            var cont = Activator.CreateInstance<TController>();
            cont.IsMain = true;
            await cont.Run();
            return new Result();
        }
    }
}
