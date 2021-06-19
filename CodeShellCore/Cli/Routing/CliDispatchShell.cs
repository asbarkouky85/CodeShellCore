using CodeShellCore.Cli.Routing.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Cli.Routing
{
    public abstract class CliDispatchShell : Shell
    {
        public static string ConfigurationApiPath
        {
            get { return _configurationApiPath; }
            set
            {
                _configurationApiPath = value;
                //ConfigureCurrent();
            }
        }
        protected ICliDispatcherBuilder Builder = new CliDispatcherBuilder();
        protected readonly string[] Arguments;

        private static string _configurationApiPath;
        private IServiceProvider __scoped;
        private IConfigurationRoot _configRoot;

        protected override bool useLocalization => false;
        protected override string appRoot => ".";
        protected override CultureInfo defaultCulture => new CultureInfo("en");
        protected override IServiceProvider _scopedProvider => __scoped;
        protected override IConfiguration Configuration => _configRoot;

        protected override IServiceProvider buildRootProvider()
        {
            ServiceCollection collection = new ServiceCollection();
            RegisterHandlers(Builder);
            var dhs = Builder.GetStartupHandlers();
            foreach (var dh in dhs)
            {
                var h = (ICliRequestHandler)Activator.CreateInstance(dh);
                var t = h.HandleAsync(Arguments);
                t.Wait();
            }
            ConfigureCurrent();
            App.RegisterServices(collection);
            return collection.BuildServiceProvider();
        }

        protected override IConfigurationSection getConfig(string key)
        {
            return _configRoot.GetSection(key);
        }

        public CliDispatchShell(string[] args)
        {
            Arguments = args;
        }

        private static void ConfigureCurrent()
        {
            if (App is CliDispatchShell)
                (App as CliDispatchShell).BuildConfiguration();
        }

        protected void BuildConfiguration()
        {
            var conf = new ConfigurationBuilder();

            EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            conf.AddJsonFile($"appsettings.json", true, true);
            conf.AddJsonFile($"appsettings.{EnvironmentName}.json", true, true);

            if (ConfigurationApiPath != null)
            {
                var f1 = Path.Combine(ConfigurationApiPath, "appsettings.json");
                var f2 = Path.Combine(ConfigurationApiPath, $"appsettings.{EnvironmentName}.json");
                conf.AddJsonFile(f1, true, true);
                conf.AddJsonFile(f2, true, true);
            }

            if (EnvironmentName != null)
                Console.WriteLine("Using environment : " + EnvironmentName);
            _configRoot = conf.Build();

        }

        protected abstract void RegisterHandlers(ICliDispatcherBuilder builder);
        public override void RegisterServices(IServiceCollection coll)
        {
            
            foreach (var h in Builder.HandlerTypes)
            {
                coll.AddTransient(h);
            }
            base.RegisterServices(coll);
            
        }

        public virtual async Task DispatchAsync()
        {
            Start(this);
            if (Arguments.Length < 1)
                return;

            using (var sc = GetScope())
            {
                __scoped = sc.ServiceProvider;
                var h = Builder.GetHandler(Arguments[0], sc.ServiceProvider);
                if (h == null)
                    return;
                await h.HandleAsync(Arguments);
            }


        }
    }
}
