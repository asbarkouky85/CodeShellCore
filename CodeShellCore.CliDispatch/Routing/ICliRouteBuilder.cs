using System;
using System.Collections.Generic;

namespace CodeShellCore.CliDispatch.Routing
{
    public interface ICliRouteBuilder
    {
        Dictionary<string, Type> HandlerDictionary { get; }
        void AddDefaultHandler<T>() where T : class, ICliRequestHandler;
        void AddHandler<T>(string functionName) where T : class, ICliRequestHandler;
        void AddHandler<T>(string functionName, object extraArgs) where T : class, ICliRequestHandler;
        ICliRequestHandler? GetHandler(string? func, IServiceProvider prov);
        IEnumerable<Type> HandlerTypes { get; }
    }
}