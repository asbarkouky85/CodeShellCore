using CodeShellCore.Services;
using CodeShellCore.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Cli
{
    public class StandaloneConsoleService : ConsoleService
    {
        protected InstanceStore<object> Store;
        public StandaloneConsoleService(IServiceProvider provider)
        {
            Store = new InstanceStore<object>(provider);
            Out = Store.GetInstance<IOutputWriter>();
        }

        protected virtual T GetService<T>() => Store.GetInstance<T>();
    }
}
