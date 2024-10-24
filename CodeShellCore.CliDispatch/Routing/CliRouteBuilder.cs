using CodeShellCore.Text;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.CliDispatch.Routing
{
    internal class CliRouteBuilder : ICliRouteBuilder
    {
        private Dictionary<string, Type> _handlerList = new Dictionary<string, Type>();
        private Dictionary<string, Dictionary<string, string>> _extraArgs = new Dictionary<string, Dictionary<string, string>>();

        public IEnumerable<Type> HandlerTypes => _getHandlerTypes();
        public Dictionary<string, Type> HandlerDictionary => _handlerList;
        IServiceCollection _services;

        public CliRouteBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public void AddHandler<T>(string functionName) where T : class, ICliRequestHandler
        {
            _handlerList[functionName] = typeof(T);
            _services?.AddTransient<T>();
        }

        public void AddHandler<T>(string functionName, object extraArgs) where T : class, ICliRequestHandler
        {
            _handlerList[functionName] = typeof(T);
            _extraArgs[functionName] = extraArgs.ToJson().FromJson<Dictionary<string, string>>();
            _services?.AddTransient<T>();
        }

        public ICliRequestHandler? GetHandler(string? func, IServiceProvider prov)
        {
            if (string.IsNullOrEmpty(func))
                func = "__default";

            if (_handlerList.TryGetValue(func, out Type? t))
            {
                if (t != null)
                {
                    var handler = (ICliRequestHandler?)prov.GetService(t);

                    if (handler != null && _extraArgs.TryGetValue(func, out Dictionary<string, string>? args))
                        handler.AddArgs(args);

                    return handler;
                }
                
            }
            return null;
        }

        private IReadOnlyList<Type> _getHandlerTypes()
        {
            var lst = _handlerList.Select(e => e.Value).ToList();
            return lst.ToArray();
        }

        public void AddDefaultHandler<T>() where T : class, ICliRequestHandler
        {
            AddHandler<T>("__default");
        }
    }
}
