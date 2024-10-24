using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore
{
    public class CodeShellApplicationInitializationContext
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }
        public string[] Arguments { get; private set; }
        private Dictionary<string, object> _extraProperties = new Dictionary<string, object>();

        public CodeShellApplicationInitializationContext(IServiceProvider serviceProvider, IConfiguration configuration, string[] arguments = null)
        {
            ServiceProvider = serviceProvider;
            Configuration = configuration;
            Arguments = arguments;
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
