using CodeShellCore.Services;
using CodeShellCore.Text.Localization;
using CodeShellCore.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Cli
{
    public class StandaloneConsoleService : ConsoleService
    {
        protected InstanceStore Store;
        protected Language Language => Store.GetService<Language>();
        public StandaloneConsoleService(IServiceProvider provider)
        {
            Store = new InstanceStore(provider);
            Out = Store.GetRequiredService<IOutputWriter>();
        }

        protected virtual T GetService<T>() => Store.GetRequiredService<T>();
    }
}
