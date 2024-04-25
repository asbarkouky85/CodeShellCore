using CodeShellCore.Security.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;

namespace CodeShellCore.Cli
{
    public abstract class ConsoleShell : Shell
    {
        //private IServiceProvider _serviceProvider;
        private IConfiguration _configRoot;
        protected override CultureInfo defaultCulture { get { return new CultureInfo("en"); } }
        protected override IConfiguration Configuration => _configRoot;
        protected override string appRoot
        {
            get
            {
                var s = AppContext.BaseDirectory;
                return s;
            }
        }

        protected override bool useLocalization { get { return false; } }
        protected virtual JsonSerializerSettings JsonSerializerSettings => _getJsonSettings();
        protected override string sharedPathRoot => ".";
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

        private JsonSerializerSettings _getJsonSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
        public static void Start<T>(Shell shell) where T : ConsoleController
        {
            Start(shell);
            JsonConvert.DefaultSettings = () => ((ConsoleShell)App).JsonSerializerSettings;

            T cont = Activator.CreateInstance<T>();
            cont.IsMain = true;
            cont.Run();
        }

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            coll.AddCodeShellApplication();
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
