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

        protected ICliDispatcherBuilder Builder = new CliDispatcherBuilder();
        protected readonly string[] Arguments;


        private IServiceProvider __scoped;
        protected IConfigurationRoot configRoot;

        protected override bool useLocalization => false;
        protected override string appRoot => ".";
        protected override CultureInfo defaultCulture => new CultureInfo("en");
        protected override IServiceProvider _scopedProvider => __scoped;
        protected override IConfiguration Configuration => configRoot;

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
            return configRoot.GetSection(key);
        }

        public static void SetSettingsPath(string path, string environment = null)
        {
            if (App is CliDispatchShell)
            {
                (App as CliDispatchShell).LoadConfigFrom(path, environment);
            }
        }

        public CliDispatchShell(string[] args)
        {
            Arguments = args;
        }

        private static void ConfigureCurrent()
        {
            if (App is CliDispatchShell)
            {
                var sh = App as CliDispatchShell;
                var builder = new ConfigurationBuilder();
                (App as CliDispatchShell).BuildConfiguration(builder);
                sh.configRoot = builder.Build();
            }

        }

        protected virtual void BuildConfiguration(ConfigurationBuilder conf)
        {
            EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            conf.AddJsonFile($"appsettings.json", true, true);
            conf.AddJsonFile($"appsettings.{EnvironmentName}.json", true, true);

            if (EnvironmentName != null)
                Console.WriteLine("Using environment : " + EnvironmentName);
        }



        public virtual void LoadConfigFrom(string path, string environment = null)
        {
            var conf = new ConfigurationBuilder();
            BuildConfiguration(conf);
            Console.WriteLine("Reading config from [" + path + "\\appsettings" + (environment == null ? "" : environment) + ".json");
            conf.AddJsonFile(Path.Combine(path, $"appsettings.json"), true, true);
            if (environment != null)
            {
                conf.AddJsonFile(Path.Combine(path, $"appsettings.{environment}.json"), true, true);
            }

            configRoot = conf.Build();
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
            {
                Console.WriteLine("No function provided");
                return;
            }
                

            using (var sc = GetScope())
            {
                __scoped = sc.ServiceProvider;
                var h = Builder.GetHandler(Arguments[0], sc.ServiceProvider);
                if (h == null) 
                {
                    Console.WriteLine("Unknow function : " + Arguments[0]);
                    return;
                }
                
                await h.HandleAsync(Arguments);
            }


        }
    }
}
