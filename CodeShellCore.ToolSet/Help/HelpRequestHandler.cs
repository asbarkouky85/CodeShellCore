using CodeShellCore.Cli;
using CodeShellCore.CliDispatch.Parsing;
using CodeShellCore.CliDispatch.Routing;
using CodeShellCore.Helpers;
using System;
using System.Reflection;
using System.Threading.Tasks;
using CodeShellCore.Types;
using System.Threading;

namespace CodeShellCore.ToolSet.Help
{
    public class HelpRequestHandler : CliRequestHandler<HelpRequest>
    {
        public override string FunctionDescription => "";
        public HelpRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        protected override void Build(ICliRequestBuilder<HelpRequest> builder)
        {

        }

        protected override Task<Result> HandleAsync(HelpRequest request, CancellationToken t)
        {
            var build = GetService<ICliRouteBuilder>();
            Console.WriteLine();
            Console.WriteLine($"Toolset v{Assembly.GetEntryAssembly().GetVersionString()}");
            Console.WriteLine();
            foreach (var item in build.HandlerDictionary)
            {
                if (item.Value == GetType())
                    continue;
                ICliRequestHandler handler = (ICliRequestHandler)Activator.CreateInstance(item.Value, ServiceProvider);
                using (ColorSetter.Set(ConsoleColor.Yellow))
                {
                    Console.Write(item.Key);
                }
                using (ColorSetter.Set(ConsoleColor.White))
                {
                    Console.WriteLine(" :\t" + handler.FunctionDescription);
                }

                Console.WriteLine("------------------------------------------------");
                handler.Document();
                Console.WriteLine();
            }
            return Task.FromResult(new Result());
        }
    }
}
