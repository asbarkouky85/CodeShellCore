using System;
using System.Collections.Generic;

namespace CodeShellCore.Cli.Routing
{
    public interface ICliDispatcherBuilder
    {
        Dictionary<string, Type> HandlerDictionary { get; }
        void AddHandler<T>(string functionName) where T : class, ICliRequestHandler;
        ICliRequestHandler GetHandler(string func, IServiceProvider prov);
        IEnumerable<Type> HandlerTypes { get; }
    }
}