using Asga.Auth;
using CodeShellCore.Cli;
using CodeShellCore.DependencyInjection;
using Microsoft.EntityFrameworkCore.InMemory;
using CodeShellCore.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CodeShellCore;
using Microsoft.AspNetCore.Http;
using CodeShellCore.Security.Cryptography;
using CodeShellCore.UnitTest;

namespace CodeShellCore.UnitTest
{
    public class UnitTestShell : Shell
    {
        protected override bool useLocalization => false;

        protected override string appRoot => ".";

        protected override CultureInfo defaultCulture => new CultureInfo("en");

        protected override IServiceProvider _scopedProvider => CurrentScope?.ServiceProvider;
        protected override IServiceProvider rootProvider => _rootProv;
        public static IServiceScope CurrentScope;

        IConfigurationRoot _configRoot;
        IServiceProvider _rootProv;
        void InitializeConfig()
        {
            var c = new ConfigurationBuilder();

            _configRoot = c.Build();
        }

        public UnitTestShell()
        {
            var coll = new ServiceCollection();

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
            coll.AddAuthModule();
            coll.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            coll.AddSingleton(d => new Encryptor("123"));
            coll.AddContext<AuthContext>();
            coll.AddDbContext<AuthContext>(d => d.UseInMemoryDatabase("mydb"));
        }
    }
}
