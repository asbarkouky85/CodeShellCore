using System;
using System.Collections.Generic;
using System.Globalization;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.UnitTest
{
    public class UnitTestShell : Shell
    {
        protected override bool useLocalization => false;
        protected override string appRoot => ".";
        protected override CultureInfo defaultCulture => new CultureInfo("en");
        protected override IServiceProvider _scopedProvider => CurrentScope?.ServiceProvider;
        protected override IServiceProvider rootProvider => _rootProv;

        protected override IConfiguration Configuration => _configRoot;

        protected override string sharedPathRoot => throw new NotImplementedException();

        private readonly Action<CodeshellAppContext> _otherRegistration;

        public static IServiceScope CurrentScope;

        IConfigurationRoot _configRoot;
        IServiceProvider _rootProv;
        void InitializeConfig()
        {
            var c = new ConfigurationBuilder();
            _configRoot = c.Build();
        }

        public UnitTestShell(Action<CodeshellAppContext> action = null)
        {
            var coll = new ServiceCollection();

            _otherRegistration = action;
            RegisterServices(coll);
            _rootProv = coll.BuildServiceProvider();
        }

        protected override IConfigurationSection getConfig(string key)
        {
            IConfigurationProvider p = new TestConfigurationProvider();
            return new ConfigurationSection(new ConfigurationRoot(new List<IConfigurationProvider> { p }), key)
            {
                Value = ""
            };
        }

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            _otherRegistration?.Invoke(new CodeshellAppContext(coll, _configRoot));
        }
    }
}
