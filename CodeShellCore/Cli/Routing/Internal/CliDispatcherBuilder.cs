using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Cli.Routing.Internal
{
    internal class CliDispatcherBuilder : ICliDispatcherBuilder
    {
        private List<Type> _defaultHandlers = new List<Type>();
        private Dictionary<string, Type> _handlerList = new Dictionary<string, Type>();

        public IEnumerable<Type> HandlerTypes => _getHandlerTypes();

        public IEnumerable<Type> GetStartupHandlers()
        {
            return _defaultHandlers;
        }

        public void AddStartupHandler<T>() where T: class,ICliRequestHandler
        {
            _defaultHandlers.Add(typeof(T));
        }

        public void AddHandler<T>(string functionName) where T : class, ICliRequestHandler
        {
            _handlerList[functionName] = typeof(T);
        }

        public ICliRequestHandler GetHandler(string func, IServiceProvider prov)
        {
            if (_handlerList.TryGetValue(func, out Type t))
            {
                return (ICliRequestHandler)prov.GetService(t);
            }
            return null;
        }

        private IReadOnlyList<Type> _getHandlerTypes()
        {
            var lst = _handlerList.Select(e => e.Value).ToList();
            lst.AddRange(_defaultHandlers);
            return lst.ToArray();
        }

    }
}
