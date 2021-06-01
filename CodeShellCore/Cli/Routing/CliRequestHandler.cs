using CodeShellCore.Cli.Routing;
using CodeShellCore.Cli.Routing.Internal;
using CodeShellCore.Helpers;
using CodeShellCore.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Cli.Routing
{
    public abstract class CliRequestHandler<T> : ConsoleService, ICliRequestHandler where T : class
    {
        IEnumerable<ArgumentItem<T>> _keyList;
        private InstanceStore<object> _store;

        protected CliRequestHandler(IServiceProvider provider)
        {
            Out = provider.GetRequiredService<IOutputWriter>();
            _store = new InstanceStore<object>(provider);
        }

        protected TService GetService<TService>() where TService : class
        {
            return _store.GetInstance<TService>();
        }

        protected abstract void Build(ICliRequestBuilder<T> builder);
        protected abstract Task<Result> HandleAsync(T request);

        public virtual T GetRequestData(string[] args)
        {
            var parser = new CliArgumentParser<T>();
            Build(parser.Builder);
            return parser.Parse(args);
        }

        public virtual Task<Result> HandleAsync(string[] args)
        {
            var req = GetRequestData(args);
            if (req == null)
            {
                return Task.FromResult(new Result(1));
            }

            return HandleAsync(req);
        }


    }
}
