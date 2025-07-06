using CodeShellCore.Cli;
using CodeShellCore.CliDispatch.Parsing;
using CodeShellCore.Helpers;
using CodeShellCore.Text;
using CodeShellCore.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShellCore.CliDispatch.Routing
{
    public abstract class CliRequestHandler<T> : ConsoleService, ICliRequestHandler where T : class
    {
        public abstract string FunctionDescription { get; }
        public virtual bool RunInBackground => false;
        protected InstanceStore<object> Store { get; private set; }
        protected IServiceProvider ServiceProvider { get; private set; }
        protected Dictionary<string, string> ExtraArgs { get; set; } = new Dictionary<string, string>();


        protected CliRequestHandler(IServiceProvider provider)
        {
            ServiceProvider = provider;
            Out = provider.GetRequiredService<IOutputWriter>();
            Store = new InstanceStore<object>(provider);
        }

        protected TService GetService<TService>() where TService : class
        {
            return Store.GetInstance<TService>();
        }

        protected object GetService(Type t)
        {
            return Store.GetInstance(t);
        }

        protected abstract void Build(ICliRequestBuilder<T> builder);
        protected abstract Task<Result> HandleAsync(T request, CancellationToken token);

        public virtual T GetRequestData(string[] args)
        {
            var parser = new CliArgumentParser<T>();
            Build(parser.Builder);
            return parser.Parse(args);
        }

        public virtual async Task<Result> HandleAsync(string[] args, CancellationToken token)
        {

            var req = GetRequestData(args);
            if (req == null)
            {
                return new Result(1);
            }
            return await HandleAsync(req, token);
        }

        public void Document()
        {
            var parser = new CliArgumentParser<T>();
            Build(parser.Builder);
            parser.Builder.Document();
        }

        public void AddArgs(Dictionary<string, string> args)
        {
            foreach (var arg in args)
            {
                ExtraArgs[arg.Key] = arg.Value;
            }
        }
    }
}
