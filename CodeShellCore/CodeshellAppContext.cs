using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore
{
    public class CodeshellAppContext
    {
        public IServiceCollection Services { get; private set; }
        public IConfiguration Configuration { get; private set; }
        private Dictionary<string, object> _extraProperties = new Dictionary<string, object>();

        public CodeshellAppContext(IServiceCollection coll, IConfiguration conf)
        {
            Services = coll;
            Configuration = conf;
        }

        public void AddItem<T>(T item) where T : class
        {
            _extraProperties[typeof(T).Name] = item;
        }

        public T GetItem<T>() where T : class
        {
            if (_extraProperties.TryGetValue(typeof(T).Name, out object item))
                return (T)item;
            return null;
        }
    }
}
