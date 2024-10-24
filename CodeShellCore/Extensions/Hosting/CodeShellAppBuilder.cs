using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.Extensions.Hosting
{
    public class CodeShellAppBuilder : IAppBuilder
    {
        IConfigurationRoot _configRoot;
        ICodeShellContainerBuilder host;
        string[] _arguments;

        public CodeShellAppBuilder(string[] args)
        {
            _arguments = args;
            Utils.SetEnvironmentFromArguments(args);
        }

        private IConfigurationRoot _defaultBuilder()
        {
            var conf = new ConfigurationBuilder();

            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            conf.AddJsonFile($"appsettings.json", true, true);
            conf.AddJsonFile($"appsettings.{environmentName}.json", true, true);
            if (environmentName != null)
                Console.WriteLine("Using environment : " + environmentName);
            return conf.Build();
        }

        public IAppBuilder UseModule<T>(Func<IConfigurationRoot> configBuilder = null) where T : CodeShellModule
        {
            if (configBuilder != null)
            {
                _configRoot = configBuilder();
            }
            else
            {
                _configRoot = _defaultBuilder();
            }
            host = new CodeShellContainerBuilder<T>(_configRoot, _arguments);
            return this;
        }


        public void RegisterServices(IServiceCollection collection)
        {
            host.RegisterServices(collection);
        }

        public IServiceProvider BuildServiceProvider()
        {
            return host.BuildServiceProvider();
        }
    }
}
