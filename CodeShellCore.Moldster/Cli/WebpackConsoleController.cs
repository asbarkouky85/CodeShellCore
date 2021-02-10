using CodeShellCore.Cli;
using CodeShellCore.Moldster.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Moldster.Cli
{
    public class WebpackConsoleController : ConsoleController
    {
        protected bool Lazy { get; private set; }
        IBundlingService Bundling => GetService<IBundlingService>();
        IDataService Data => GetService<IDataService>();
        IPublisherService Publisher => GetService<IPublisherService>();
        IPathsService paths => GetService<IPathsService>();
        public override Dictionary<int, string> Functions
        {
            get
            {
                return new Dictionary<int, string>
                {
                    { 1,"WriteWebPackFiles"},
                    { 2,"PrepEnvironment"},
                    { 3,"BundleTenant"},
                    { 4,"UploadBundle"},
                    { 5,"ClearOlderBundles"},
                    { 6,"DevelopmentPack"}
                };
            }
        }

        public void WriteWebPackFiles()
        {
            Bundling.WriteWebpackConfigFiles(Lazy);
        }

        public void PrepEnvironment()
        {
            var packProd = GetBoolFromUser("Pack vendor production ", false);
            Bundling.PrepEnvironment(packProd);
        }

        public void DevelopmentPack()
        {
            Bundling.DevelopmentPack();
        }

        public void UploadBundle()
        {
            while (true)
            {
                var envs = paths.GetEnvironments();
                string envName = GetSelectionFromUser("Select environment ", envs.Select(d => d.Name).ToArray());
                if (envName == null)
                    break;
                Injector.GetService<EnvironmentAccessor>().CurrentEnvironment = envs.Where(d => d.Name == envName).First();

                while (true)
                {
                    var ten = Data.GetAppCodes();
                    string modCode = GetSelectionFromUser("Select Module", ten);
                    if (modCode == null)
                        break;

                    string version = Bundling.GetAppVersion(modCode);

                    Publisher.UploadTenantBundle(modCode, "v" + version);

                    var changeVersion = GetBoolFromUser("Apply version change on server ", true);
                    if (changeVersion)
                    {
                        Publisher.SetTenantInfo(modCode, version);
                    }
                }
            }
        }

        public void BundleTenant()
        {
            while (true)
            {
                List<string> lst = new List<string> { "All" };
                var ten = Data.GetAppCodes();
                lst.AddRange(ten);
                string modCode = GetSelectionFromUser("Select Module", lst.ToArray());
                if (modCode == null)
                    break;
                if (modCode == "All")
                {
                    foreach (var b in ten)
                        Bundling.ProductionPack(b);
                }
                else
                {
                    Bundling.ProductionPack(modCode);
                }
               
            }
        }

        public void ClearOlderBundles()
        {
            while (true)
            {
                var envs = paths.GetEnvironments();
                envs.Add(new Definitions.MoldsterEnvironment { Name = "(CurrentMachine)", Upload = new Net.UploadConfig { Type = "DEV" } });
                string envName = GetSelectionFromUser("Select environment ", envs.Select(d => d.Name).ToArray());
                if (envName == null)
                    break;
                Injector.GetService<EnvironmentAccessor>().CurrentEnvironment = envs.Where(d => d.Name == envName).First();

                while (true)
                {
                    var ten = Data.GetAppCodes();
                    string modCode = GetSelectionFromUser("Select Module", ten);
                    if (modCode == null)
                        break;

                    Publisher.DeleteOtherBundlesForTenant(modCode);
                }
            }

        }
    }
}
