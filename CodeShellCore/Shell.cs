using CodeShellCore.Files.Logging;
using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.MQ;
using CodeShellCore.Security.Cryptography;
using CodeShellCore.Text;
using CodeShellCore.Text.Localization;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;

namespace CodeShellCore
{
    public abstract class Shell : IDisposable
    {

        private string _reportRoot;
        private static Encryptor _encryptor;
        private static CodeShellAppOptions _appOptions;
        private static readonly object _locker = new object();
        public static string EnvironmentName { get; protected set; }
        public static string SolutionFolder { get; private set; }
        public static Assembly ProjectAssembly { get; private set; }
        public static IServiceProvider RootInjector { get; private set; }
        public static IConfiguration RootConfiguration { get; private set; }
        public static string LocalizationAssembly => _getOptions().LocalizationAssembly;
        public static bool UseLocalization => _getOptions().UseLocalization;
        public static bool UseMultiTenancy => _getOptions().UseMultiTenancy;
        public static CultureInfo DefaultCulture => new CultureInfo(_getOptions().DefaultCulture);
        public static IEnumerable<string> SupportedLanguages => _getOptions().SupportedLanguages;
        public static string AppRootPath { get; private set; } = ".";
        public static string PublicRoot => _getOptions().PublicRoot;
        public static string ReportsRoot => _getOptions().ReportsRoot;
        public static string AuthServiceProvider => GetConfigAs<string>("AuthServer", false);

        static Shell()
        {
            ProjectAssembly = Assembly.GetEntryAssembly();
            EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            SolutionFolder = AppDomain.CurrentDomain.BaseDirectory.GetBeforeFirst("\\" + ProjectAssembly.GetName().Name);
        }


        // public static IServiceProvider ScopedInjector { get { return App._scopedProvider; } }


        private static CodeShellAppOptions _getOptions()
        {
            var section = RootConfiguration.GetSection(ConfigNames.CodeShellApp);
            if (section.Exists())
            {
                _appOptions = section.Get<CodeShellAppOptions>();
            }
            else
            {
                _appOptions = new CodeShellAppOptions();
            }
            return _appOptions;
        }

        public static Encryptor Encryptor
        {
            get
            {
                lock (_locker)
                {
                    if (_encryptor == null)
                    {
                        var key = GetConfig(ConfigNames.AuthenticationEncKey);
                        if (key.Value == null)
                            throw new CodeShellHttpException(HttpStatusCode.InternalServerError, "Encryption requires AuthenticationEncKey in configuration");
                        _encryptor = new Encryptor(key.Value);
                    }
                    return _encryptor;
                }
            }
        }


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

        protected abstract bool useLocalization { get; }
        protected abstract string appRoot { get; }
        protected abstract CultureInfo defaultCulture { get; }
        protected abstract IServiceProvider _scopedProvider { get; }


        public virtual void RegisterServices(IServiceCollection coll)
        {

        }
        protected abstract IConfigurationSection getConfig(string key);
        protected virtual void OnReady()
        {

        }

        public virtual void Dispose()
        {
            Logger.Default?.Dispose();
            if (useTransporter)
                Transporter.Exit();

        }

        /// <summary>
        /// Gets a Scoped <see cref="IServiceProvider"/> and runs on startup after all registration is done (Use for migrations and seeding)
        /// </summary>
        /// <param name="prov"></param>
        protected virtual void OnApplicationStarted(IServiceProvider prov)
        {

        }


        #region Static Methods
        public static void Start(Shell cont)
        {

        }


        public static IConfigurationSection GetConfig(string key, bool required = true)
        {
            var val = RootConfiguration.GetSection(key);
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

        public static IServiceScope GetScope(CultureInfo cult = null)
        {
            var sc = RootInjector.CreateScope();
            if (cult != null)
                sc.ServiceProvider.GetService<Language>().SetCulture(cult.TwoLetterISOLanguageName);

            return sc;
        }

        public static void SetRootProvider(IServiceProvider rootProvider)
        {
            RootInjector = rootProvider;
        }

        internal static void SetConfigRoot(IConfiguration configuration)
        {
            RootConfiguration = configuration;
        }

        internal static void SetEnvironmentName(string name)
        {
            EnvironmentName = name;
        }

        public static void SetRootPath(string path)
        {
            AppRootPath = path;
        }



        #endregion

    }
}
