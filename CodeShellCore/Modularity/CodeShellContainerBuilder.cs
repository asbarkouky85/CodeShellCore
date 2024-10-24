using CodeShellCore.Cli;
using CodeShellCore.Files.Logging;
using CodeShellCore.Modularity;
using CodeShellCore.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore
{
    public class CodeShellContainerBuilder<TModule> : ICodeShellContainerBuilder where TModule : CodeShellModule
    {
        private IServiceCollection _serviceCollection;
        protected IConfiguration Configuration;
        protected IServiceProvider RootProvider { get; set; }
        private List<Type> _registeredModule = new List<Type>();
        private List<Type> _configuredModules = new List<Type>();
        private List<Type> _startedModules = new List<Type>();
        private List<Type> _closedModule = new List<Type>();
        private string[] _arguments;
        public CodeShellContainerBuilder(IConfiguration configuration, string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += (e, s) =>
            {
                StopModule(typeof(TModule));
            };

            Logger.Set(Shell.ProjectAssembly.GetName().Name);
            string envName = Shell.EnvironmentName == null ? "" : "-" + Shell.EnvironmentName;
            Console.Title = Shell.ProjectAssembly.GetName().Name + "-v" + Shell.ProjectAssembly.GetVersionString() + envName;

            Configuration = configuration;
            Shell.SetConfigRoot(Configuration);
            _arguments = args;
        }

        protected void RegisterModule(Type module, CodeshellAppContext context)
        {
            if (!_registeredModule.Contains(module))
            {
                var deps = module.GetDependsOnModules();
                foreach (var dep in deps)
                {
                    RegisterModule(dep, context);
                }
                var mod = (CodeShellModule)Activator.CreateInstance(module);
                mod.RegisterServices(context);
                _registeredModule.Add(module);
            }
        }

        protected void ConfigureModule(Type module, CodeShellApplicationInitializationContext context)
        {
            if (!_configuredModules.Contains(module))
            {
                var mod = (CodeShellModule)Activator.CreateInstance(module);
                mod.Configure(context);

                var deps = module.GetDependsOnModules().Reverse();
                foreach (var dep in deps)
                {
                    ConfigureModule(dep, context);
                }
                _configuredModules.Add(module);
            }
        }

        protected void StartModule(Type module)
        {
            if (!_startedModules.Contains(module))
            {
                var deps = module.GetDependsOnModules();
                foreach (var dep in deps)
                {
                    StartModule(dep);
                }
                var mod = (CodeShellModule)Activator.CreateInstance(module);
                mod.OnApplicationStarted(new CodeShellApplicationInitializationContext(RootProvider, Configuration, _arguments));
                _startedModules.Add(module);
            }
        }

        protected void StopModule(Type module)
        {
            if (!_closedModule.Contains(module))
            {
                var deps = module.GetDependsOnModules();
                foreach (var dep in deps)
                {
                    StopModule(dep);
                }
                var mod = (CodeShellModule)Activator.CreateInstance(module);
                mod.OnApplicationStopped();
                _closedModule.Add(module);
            }
        }

        public void RegisterServices(IServiceCollection collection)
        {
            _serviceCollection = collection;

            var context = new CodeshellAppContext(collection, Configuration, _arguments);
            RegisterModule(typeof(TModule), context);
        }

        public IServiceProvider BuildServiceProvider()
        {
            var serviceProvider = _serviceCollection.BuildServiceProvider();
            SetRootProvider(serviceProvider);

            var initContext = new CodeShellApplicationInitializationContext(RootProvider, Configuration, _arguments);
            ConfigureModule(typeof(TModule), initContext);

            var hostApplicationLifeTime = serviceProvider.GetService<IHostApplicationLifetime>();

            if (hostApplicationLifeTime != null)
            {
                hostApplicationLifeTime.ApplicationStarted.Register(() => StartModule(typeof(TModule)));
                hostApplicationLifeTime.ApplicationStopped.Register(() => StopModule(typeof(TModule)));
            }

            return serviceProvider;
        }

        protected void SetRootProvider(IServiceProvider applicationServices)
        {
            RootProvider = applicationServices;
            Shell.SetRootProvider(RootProvider);
        }

        public void Run()
        {
            StartModule(typeof(TModule));
        }
    }
}
