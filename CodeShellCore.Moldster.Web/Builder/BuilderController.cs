using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Builder;
using CodeShellCore.Moldster.Environments.Services;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Sql;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.Builder
{
    public class BuilderController : BaseApiController
    {
        IInitializationService Initialize => GetService<IInitializationService>();
        IBundlingService Bundling => GetService<IBundlingService>();
        ILegacyBundlingService LegacyBundling => GetService<ILegacyBundlingService>();
        IPublisherService Publisher => GetService<IPublisherService>();
        IPathsService paths => GetService<IPathsService>();
        ILocalizationService Localization => GetService<ILocalizationService>();

        public async Task<Result> Init(bool? replace = null)
        {
            Initialize.AddBasicFiles(replace == true);
            return await RespondAsync();
        }

        public async Task<Result> AddShellComponents(bool? replace = null)
        {
            Initialize.AddShellComponents(replace == true);
            return await RespondAsync();
        }

        public async Task<Result> AddStaticFiles(bool? replace = null)
        {
            Initialize.AddStaticFiles(replace == true);
            return await RespondAsync();
        }

        public async Task<Result> AddBaseScripts(bool? replace = null)
        {
            Initialize.AddCodeShell(replace == true);
            return await RespondAsync();
        }
        public async Task<Result> WriteWebPackFiles()
        {
            await LegacyBundling.WriteWebpackConfigFiles();
            return await RespondAsync();
        }

        public async Task<Result> PrepEnvironment(bool packProd)
        {
            await Bundling.PrepEnvironment(packProd);
            return await RespondAsync();
        }

        public async Task<SubmitResult> ClearOlderBundles([FromBody] DbCreationRequest req)
        {
            var service = GetService<IPageParameterDataService>();
            return await service.UpdateTemplatePagesViewParamsJson(req.TenantCode);

            //var envs = paths.GetEnvironments();
            //MoldsterEnvironment env = null;
            //if (req.Environment == "(Current Machine)")
            //{
            //    env = MoldsterEnvironment.Development;
            //}
            //else
            //{
            //    env = envs.Where(d => d.Name == req.Environment).First();
            //}

            //GetService<EnvironmentAccessor>().CurrentEnvironment = env;
            //Publisher.DeleteOtherBundlesForTenant(req.TenantCode);
            //return Respond();
        }

        public async Task<Result> InitializeResx()
        {
            await Localization.AddLocalizationFiles();
            return await RespondAsync();
        }

        public async Task<Result> FixPages()
        {
            await Localization.FixPages(null);
            return await RespondAsync();
        }

        public async Task<Result> SyncLanguages()
        {

            await Localization.SyncAllLanguages();
            return await RespondAsync();
        }
    }
}
