using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeShellCore.Web.Services
{
    public class ConsoleHostingEnvironment : IHostingEnvironment
    {
        private IFileProvider _webProvider;
        private IFileProvider _contentProvider;
        private string _root;
        private string _contentRoot;
        private string _appName;
        private string _envName;
        public ConsoleHostingEnvironment()
        {
            _envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            _appName = GetType().Assembly.GetName().Name;
            _root = System.Environment.CurrentDirectory;
            _contentRoot = Environment.CurrentDirectory;
            _webProvider = new PhysicalFileProvider(_root);
            _contentProvider = new PhysicalFileProvider(_contentRoot);
        }
        public string EnvironmentName
        {
            get => _envName;
            set => _envName = value;
        }
        public string ApplicationName
        {
            get => _appName;
            set => _appName = value;
        }
        public string WebRootPath
        {
            get => _root;
            set => _root = value;
        }
        public IFileProvider WebRootFileProvider
        {
            get => _webProvider;
            set => _webProvider = value;
        }
        public string ContentRootPath
        {
            get => _contentRoot;
            set => _contentRoot = value;
        }
        public IFileProvider ContentRootFileProvider
        {
            get => _contentProvider;
            set => _contentProvider = value;
        }
    }
}
