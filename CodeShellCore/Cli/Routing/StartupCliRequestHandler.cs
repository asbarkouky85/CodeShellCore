using CodeShellCore.Cli.Routing.Internal;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Cli.Routing
{
    public abstract class StartupCliRequestHandler<T> : ICliRequestHandler where T : class
    {
        protected abstract void Build(ICliRequestBuilder<T> builder);
        protected abstract Task<Result> HandleAsync(T request);
        public virtual T GetRequestData(string[] args)
        {
            var Parser = new CliArgumentParser<T>();
            Build(Parser.Builder);
            return Parser.Parse(args);
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
