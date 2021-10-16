using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Security;
using CodeShellCore.Files.Logging;
using CodeShellCore.Security.Cryptography;
using CodeShellCore.Types;
using CodeShellCore.Http;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Text.Localization;
using CodeShellCore.Files;
using CodeShellCore.Text.TextProviders;
using CodeShellCore.Text;
using CodeShellCore.Helpers;
using CodeShellCore.Cli;
using CodeShellCore.MQ;
using CodeShellCore.Files.Uploads;
using CodeShellCore.Tasks;
using CodeShellCore.Modularity;
using System.Linq;

namespace CodeShellCore
{
    public abstract class Shell : IDisposable
    {

        #region Fields

        private IServiceProvider _rootProvider;
        private string _reportRoot;
        private static Encryptor _encryptor;
        private static readonly object _locker = new object();

        protected static Shell App;
        #endregion

        public Shell()
        {
            ProjectAssembly = GetType().Assembly;
            SolutionFolder = AppDomain.CurrentDomain.BaseDirectory.GetBeforeFirst("\\" + ProjectAssembly.GetName().Name);
            EnvironmentName = GetEnvironmentName();
        }

        #region Static Properties

        public static string EnvironmentName { get; protected set; }
        public static string SolutionFolder { get; private set; }
        public static Assembly ProjectAssembly { get; private set; }
        public static bool UseLocalization { get { return App.useLocalization; } }
        public static CultureInfo DefaultCulture { get { return App.defaultCulture; } }
        public static IEnumerable<string> SupportedLanguages { get { return App.Supordedlanguage; } }
        public static IServiceProvider RootInjector { get { return App.rootProvider; } }
        // public static IServiceProvider ScopedInjector { get { return App._scopedProvider; } }
        public static IUser User { get { return App._scopedProvider.GetCurrentUser(); } }
        public static string AppRootPath { get { return App.appRoot; } }
        public static string LocalizationAssembly { get { return App.localizationAssembly ?? ProjectAssembly.GetName().Name; } }
        public static string PublicRoot { get { return App.publicRelativePath; } }
        public static string ReportsRoot { get { return App.reportsRoot; } }


        public static Encryptor Encryptor
        {
            get
            {
                lock (_locker)
                {
                    if (_encryptor == null)
                    {
                        var key = App.getConfig(ConfigNames.AuthenticationEncKey);
                        if (key.Value == null)
                            throw new CodeShellHttpException(HttpStatusCode.InternalServerError, "Encryption requires AuthenticationEncKey in configuration");
                        _encryptor = new Encryptor(key.Value);
                    }
                    return _encryptor;
                }
            }
        }

        #endregion

        #region Optional Properties
        protected virtual bool useTransporter => false;
        protected virtual bool useTimedJobs => false;
        protected virtual IEnumerable<string> Supordedlanguage { get { return new[] { "ar", "en" }; } }
        protected virtual string publicRelativePath { get { return ""; } }
        protected virtual string localizationAssembly { get { return null; } }
        protected virtual string reportsRoot
        {
            get
            {
                if (_reportRoot == null)
                {
                    var sol = Utils.GetSolutionFolder(GetType().Assembly);
                    var conf = getConfig("ReportsRoot");
                    if (!string.IsNullOrEmpty(conf?.Value))
                        _reportRoot = (conf.Value as string).Replace("{PARENT}", sol);
                    else
                        _reportRoot = Path.Combine(appRoot, "Reports");
                }
                return _reportRoot;
            }
        }

        protected abstract IConfiguration Configuration { get; }
        protected virtual IServiceProvider rootProvider
        {
            get
            {
                if (_rootProvider == null)
                    _rootProvider = buildRootProvider();
                return _rootProvider;
            }
        }
        #endregion

        #region Required Properties
        protected abstract bool useLocalization { get; }

        protected abstract string appRoot { get; }
        protected abstract CultureInfo defaultCulture { get; }
        protected abstract IServiceProvider _scopedProvider { get; }


        #endregion

        #region Methods

        public virtual void RegisterServices(IServiceCollection coll)
        {
            
            coll.AddLogging();
            coll.AddSingleton<IFileHandler, FileSystemHandler>();
            coll.AddTransient<ILocaleTextProvider, ResxTextProvider>();

            coll.AddTransient<IOutputWriter, ConsoleOutputWriter>();

            coll.AddTransient<IUploadedFilesHandler, UploadedFileHandler>();
            coll.AddScoped<Language>();

            coll.AddScoped<ClientData>();

            

        }
        protected abstract IConfigurationSection getConfig(string key);
        protected virtual void OnReady()
        {

        }

        protected virtual string GetEnvironmentName()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        public virtual void Dispose()
        {
            Logger.Default?.Dispose();
            if (useTransporter)
                Transporter.Exit();

        }

        protected void StartJobs()
        {
            var jobs = RootInjector.GetService<JobConfig>().Jobs;
            foreach (var job in jobs)
            {
                IJobRunner runner = RootInjector.GetService<IJobRunner>();
                runner.Job = job;
                runner.Timer.Start();
            }
        }

        protected virtual IServiceProvider buildRootProvider()
        {
            ServiceCollection collection = new ServiceCollection();
            App.RegisterServices(collection);
            return collection.BuildServiceProvider();
        }

        /// <summary>
        /// Gets a Scoped <see cref="IServiceProvider"/> and runs on startup after all registration is done (Use for migrations and seeding)
        /// </summary>
        /// <param name="prov"></param>
        protected virtual void OnApplicationStarted(IServiceProvider prov)
        {

        }

        #endregion

        #region Static Methods
        public static void Start(Shell cont)
        {
            App = cont;

            Logger.Set(ProjectAssembly.GetName().Name);
            AppDomain.CurrentDomain.ProcessExit += (e, s) =>
            {
                App.Dispose();
            };
            string envName = EnvironmentName == null ? "" : "-" + EnvironmentName;
            Console.Title = ProjectAssembly.GetName().Name + "-v" + ProjectAssembly.GetVersionString() + envName;
            if (App.useTransporter)
                Transporter.Start();
            if (App.useTimedJobs)
                App.StartJobs();
            cont.OnReady();
            using (var sc = GetScope())
            {
                cont.OnApplicationStarted(sc.ServiceProvider);
            }
        }

        public static void Exit()
        {
            App.Dispose();
        }


        public static IConfigurationSection GetConfig(string key, bool required = true)
        {
            var val = App.getConfig(key);
            if (val.Value == null && required)
                throw new Exception("Config '" + key + "' is required to be present in appsettings.json");

            return val;
        }

        /// <summary>
        /// Reads configuration from appsettings.{ASPNET_ENVIRONMENT}.json
        /// </summary>
        /// <typeparam name="T">the return type; can be a value object or a reference type (class)</typeparam>
        /// <param name="key">the key from the config file</param>
        /// <param name="required">if true the method will can</param>
        /// <returns>the data in the appsettings formatted as <typeparamref name="T"/></returns>
        public static T GetConfigAs<T>(string key, bool required = true)
        {
            IConfigurationSection sec = GetConfig(key, required);

            return sec.Get<T>();
        }

        public static T GetConfigObject<T>(string key) where T : class
        {
            IConfigurationSection sec = GetConfig(key, false);

            return sec.Get<T>();
        }

        public static IServiceScope GetScope()
        {
            var sc = RootInjector.CreateScope();
            if (App._scopedProvider != null)
            {
                Language lang = App._scopedProvider.GetService<Language>();
                if (lang != null)
                    sc.ServiceProvider.GetService<Language>().SetCulture(lang.Culture.TwoLetterISOLanguageName);
            }
            return sc;
        }



        #endregion

    }
}
