using System;
using System.Collections.Generic;

namespace CodeShellCore.Cli.Routing
{
    public interface ICliDispatcherBuilder
    {
        void AddHandler<T>(string functionName) where T : class, ICliRequestHandler;
        void AddStartupHandler<T>() where T : class, ICliRequestHandler;

        IEnumerable<Type> GetStartupHandlers();
        ICliRequestHandler GetHandler(string func, IServiceProvider prov);
        IEnumerable<Type> HandlerTypes { get; }
    }
}