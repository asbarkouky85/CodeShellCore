using CodeShellCore.Cli.Routing;
using CodeShellCore.Data;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Services;
using CodeShellCore.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace CodeShellCore.Cli
{
    public abstract class ConsoleShell : Shell
    {
        //private IServiceProvider _serviceProvider;
        private IConfiguration _configRoot;
        protected override CultureInfo defaultCulture { get { return new CultureInfo("en"); } }

        protected override string appRoot
        {
            get
            {
                var s = AppContext.BaseDirectory;
                return s;
            }
        }

        protected override bool useLocalization { get { return false; } }


        public ConsoleShell()
        {
            var conf = new ConfigurationBuilder();

            EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            conf.AddJsonFile($"appsettings.json", true, true);
            conf.AddJsonFile($"appsettings.{EnvironmentName}.json", true, true);
            if (EnvironmentName != null)
                Console.WriteLine("Using environment : " + EnvironmentName);
            _configRoot = conf.Build();

        }

        public static void Start<T>(Shell shell) where T : ConsoleController
        {
            Start(shell);
            T cont = Activator.CreateInstance<T>();
            cont.IsMain = true;
            cont.Run();
        }

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            coll.AddSingleton<AuthorizationService>();
        }

        protected override IConfigurationSection getConfig(string key)
        {
            return _configRoot.GetSection(key);
        }

        public static IServiceScope CurrentScope { get; set; }

        protected override IServiceProvider _scopedProvider { get { return CurrentScope?.ServiceProvider; } }

    }
}
