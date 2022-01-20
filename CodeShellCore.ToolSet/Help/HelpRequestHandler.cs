using CodeShellCore.Cli;
using CodeShellCore.Cli.Routing;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Help
{
    public class HelpRequestHandler : CodeShellCore.Cli.Routing.CliRequestHandler<HelpRequest>
    {
        public override string FunctionDescription => "";
        public HelpRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        protected override void Build(ICliRequestBuilder<HelpRequest> builder)
        {

        }

        protected override Task<Result> HandleAsync(HelpRequest request)
        {
            var build = GetService<ICliDispatcherBuilder>();
            foreach (var item in build.HandlerDictionary)
            {
                if (item.Value == GetType())
                    continue;
                ICliRequestHandler handler = (ICliRequestHandler)Activator.CreateInstance(item.Value, ServiceProvider);
                using (ColorSetter.Set(ConsoleColor.Yellow))
                {
                    Console.Write(item.Key );
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
