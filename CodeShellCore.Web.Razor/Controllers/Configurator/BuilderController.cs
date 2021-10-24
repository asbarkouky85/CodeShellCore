using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Builder.Services;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.Environments.Services;
using CodeShellCore.Moldster.Localization.Services;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CodeShellCore.Web.Razor.Controllers.Configurator
{
    public class BuilderController : BaseApiController
    {
        IInitializationService Initialize => GetService<IInitializationService>();
        IBundlingService Bundling => GetService<IBundlingService>();
        IPublisherService Publisher => GetService<IPublisherService>();
        IPathsService paths => GetService<IPathsService>();
        ILocalizationService Localization => GetService<ILocalizationService>();

        public IActionResult Init(bool? replace = null)
        {
            Initialize.AddBasicFiles(replace == true);
            return Respond();
        }

        public IActionResult AddShellComponents(bool? replace = null)
        {
            Initialize.AddShellComponents(replace == true);
            return Respond();
        }

        public IActionResult AddSQLFiles()
        {
            return Respond();
        }

        public IActionResult AddStaticFiles(bool? replace = null)
        {
            Initialize.AddStaticFiles(replace == true);
            return Respond();
        }

        public IActionResult AddBaseScripts(bool? replace = null)
        {
            Initialize.AddCodeShell(replace == true);
            return Respond();
        }
        public IActionResult WriteWebPackFiles()
        {

            return Respond();
        }

        public IActionResult PrepEnvironment(bool packProd)
        {
            Bundling.PrepEnvironment(packProd);
            return Respond();
        }

        public IActionResult ClearOlderBundles([FromBody] DbCreationRequest req)
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
            Localization.AddLocalizationFiles();
            return Respond();
        }

        public IActionResult FixPages()
        {
            Localization.FixPages(null);
            return Respond();
        }

        public IActionResult SyncLanguages()
        {

            Localization.SyncAllLanguages();
            return Respond();
        }
    }
}
