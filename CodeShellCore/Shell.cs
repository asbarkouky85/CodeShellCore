using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Threading;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Security.Authorization;
using CodeShellCore.Security;
using CodeShellCore.Files.Logging;
using CodeShellCore.Security.Cryptography;
using CodeShellCore.Types;
using CodeShellCore.Services.Http;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Text.Localization;
using CodeShellCore.Files;

namespace CodeShellCore
{
    public abstract class Shell : IDisposable
    {

        #region Fields
        protected static Shell App;
        private static Dictionary<string, IServiceScope> Scopes = new Dictionary<string, IServiceScope>();
        private IServiceProvider _rootProvider;
        private static readonly object _locker = new object();
        private static Encryptor _encryptor;
        #endregion

        #region Static Properties

        public static IServiceProvider RootInjector { get { return App.rootProvider; } }
        public static IServiceProvider ScopedInjector { get { return App._scopedProvider; } }
        public static bool UseLocalization { get { return App.useLocalization; } }

        public static IEnumerable<string> SupportedLanguages { get { return App.Supordedlanguage; } }

        public static string LocalizationAssembly { get { return App.localizationAssembly ?? ProjectAssembly.GetName().Name; } }
        public static string ReportsRoot { get { return App.reportsRoot; } }
        public static string AppRootPath { get { return App.appRoot; } }

        public static IUser User { get { return App._scopedProvider.GetCurrentUser(); } }
        public static string PublicRoot { get { return App.publicRelativePath; } }
        public static Assembly ProjectAssembly { get { return App.GetType().Assembly; } }
        public static CultureInfo DefaultCulture { get { return App.defaultCulture; } }
        public static AuthorizationService AuthorizationService { get { return ScopedInjector.GetRequiredService<AuthorizationService>(); } }

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

        public static IServiceProvider ThreadInjector
        {
            get
            {
                string id = Thread.CurrentThread.ManagedThreadId.ToString();

                IServiceScope scope;
                if (Scopes.TryGetValue(id, out scope))
                    return scope.ServiceProvider;

                IServiceScopeFactory f = RootInjector.GetRequiredService<IServiceScopeFactory>();
                Scopes[id] = f.CreateScope();
                return Scopes[id].ServiceProvider;
            }
        }



        #endregion

        #region Optional Properties
        protected virtual string publicRelativePath { get { return ""; } }
        protected virtual string localizationAssembly { get { return null; } }
        protected virtual string reportsRoot { get { return Path.Combine(appRoot, "Reports"); } }
        protected virtual IServiceProvider rootProvider
        {
            get { if (_rootProvider == null) _rootProvider = makeProvider(); return _rootProvider; }
        }
        #endregion

        #region Required Properties
        protected abstract bool useLocalization { get; }
        protected virtual IEnumerable<string> Supordedlanguage { get { return new[] { "ar", "en" }; } }

        protected abstract string appRoot { get; }
        protected abstract CultureInfo defaultCulture { get; }
        protected abstract IServiceProvider _scopedProvider { get; }


        #endregion

        #region Methods

        public virtual void RegisterServices(IServiceCollection coll)
        {
            string conf = getConfig(ConfigNames.AuthenticationEncKey).Value;
            if (conf != null)
                coll.AddSingleton(d => new Encryptor(conf));
            coll.AddScoped<IUserAccessor, UserAccessor>();
            coll.AddSingleton<IFileHandler, FileSystemHandler>();
        }
        protected abstract IConfigurationSection getConfig(string key);
        protected virtual void OnReady() { }

        public virtual void Dispose() { }

        private IServiceProvider makeProvider()
        {
            ServiceCollection collection = new ServiceCollection();
            App.RegisterServices(collection);
            return collection.BuildServiceProvider();
        }

        #endregion

        #region Static Methods

        public static void Start(Shell cont)
        {
            App = cont;
            string path = Path.Combine(AppRootPath, "Logs");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            Logger.Set(ProjectAssembly.GetName().Name, path);

            AppDomain.CurrentDomain.ProcessExit += (e, s) => { App.Dispose(); };
            Console.Title = ProjectAssembly.GetName().Name + " (v" + ProjectAssembly.GetVersionString() + ")";
            cont.OnReady();

        }

        public static void Exit()
        {
            App.Dispose();
        }

        public static string GetUrl(string key, string currentHost = "localhost", bool https = false, bool required = true)
        {
            string subject = GetConfigAs<string>(key, required);
            if (subject != null)
            {
                subject = subject.Replace("{CURRENT_HOST}", currentHost);
                subject = subject.Replace("{CURRENT_PROTOCOL}", https ? "https" : "http");
            }
            return subject;
        }

        public static IConfigurationSection GetConfig(string key, bool required = true)
        {
            var val = App.getConfig(key);
            if (val.Value == null && required)
                throw new Exception("Config '" + key + "' is required to be present in the config file");

            return val;
        }

        public static T GetConfigAs<T>(string key, bool required = true)
        {
            IConfigurationSection sec = GetConfig(key, required);

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
