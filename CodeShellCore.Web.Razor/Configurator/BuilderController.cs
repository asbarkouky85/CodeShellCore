using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CodeShellCore.Web.Razor.Configurator
{
    public class BuilderController : BaseApiController
    {
        IBundlingService Bundling => GetService<IBundlingService>();
        IPublisherService Publisher => GetService<IPublisherService>();
        IPathsService paths => GetService<IPathsService>();
        ILocalizationService Localization => GetService<ILocalizationService>();

        public IActionResult Init()
        {
            Bundling.GenerateEnvironment();
            return Respond();
        }

        public IActionResult AddSQLFiles()
        {
            Bundling.AddSQLFiles();
            return Respond();
        }

        public IActionResult AddStaticFiles()
        {
            Bundling.AddStaticFiles();
            return Respond();
        }

        public IActionResult AddBaseScripts()
        {
            Bundling.AddCodeShell();
            return Respond();
        }
        public IActionResult WriteWebPackFiles()
        {
            Bundling.WriteWebpackConfigFiles(true);
            return Respond();
        }

        public IActionResult PrepEnvironment(bool packProd)
        {
            Bundling.PrepEnvironment(packProd);
            return Respond();
        }

        public IActionResult ClearOlderBundles([FromBody]DbCreationRequest req)
        {

            var envs = paths.GetEnvironments();
            MoldsterEnvironment env = null;
            if (req.Environment == "(Current Machine)")
            {
                env = MoldsterEnvironment.Development;
            }
            else
            {
                env = envs.Where(d => d.Name == req.Environment).First();
            }

            GetService<EnvironmentAccessor>().CurrentEnvironment = env;
            Publisher.DeleteOtherBundlesForTenant(req.TenantCode);
            return Respond();
        }

        public IActionResult InitializeResx()
        {
            Localization.InitializeResxFiles();
            return Respond();
        }

        public IActionResult SyncLanguages()
        {

            Localization.SyncLanguages("ar", "en");
            return Respond();
        }
    }
}
