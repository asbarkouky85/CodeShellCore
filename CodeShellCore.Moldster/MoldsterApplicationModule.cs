using CodeShellCore.Caching;
using CodeShellCore.Cli;
using CodeShellCore.Modularity;
using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Moldster.Tracing;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Web.Moldster.Configurator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.Moldster.Builder;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.Domains.Services;
using CodeShellCore.Moldster.Environments.Services;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages.Services;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Resources;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Views;
using CodeShellCore.Security.Authentication;

namespace CodeShellCore.Moldster
{
    [DependsOn(typeof(CodeShellApplicationModule))]
    public class MoldsterApplicationModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            
            context.Services.Configure<MoldsterModuleOptions>(e =>
            {
                e.ReplaceComponentHtml = true;
                e.ReplaceComponentScripts = true;
                e.ReplaceDomainRoutes = true;
                e.ReplaceMainRoutes = true;
                e.ReplaceMainModule = true;
                e.ReplaceAppComponentHtml = true;
            });
            context.Services.AddOptions<MoldsterModuleOptions>();
            context.Services.Configure<MoldsterModuleOptions>(context.Configuration.GetSection("Moldster"));

            var legacy = context.Configuration.GetSection("Moldster:Legacy").Get<bool>();

            if (legacy)
            {
                context.Services.AddSingleton<IMoldProvider, LegacyAngularMoldProvider>();
                context.Services.AddTransient<IBundlingService, LegacyBundlingService>();
                context.Services.AddTransient<ILegacyBundlingService, LegacyBundlingService>();
                context.Services.AddTransient<IInitializationService, LegacyInitializationService>();
                context.Services.AddTransient<IPublisherService, LegacyPublisherService>();
                context.Services.AddTransient<INamingConventionService, LegacyAngularNamingConventionService>();
                context.Services.AddTransient<IDomainScriptGenerationService, LegacyDomainScriptGenerationService>();
                context.Services.AddTransient<ITenantScriptGenerationService, LegacyTenantScriptGenerationService>();
            }
            else
            {
                context.Services.AddTransient<ILegacyBundlingService, BundlingService>();
                context.Services.AddSingleton<IMoldProvider, AngularMoldProvider>();
                context.Services.AddTransient<IBundlingService, BundlingService>();
                context.Services.AddTransient<IInitializationService, InitializationService>();
                context.Services.AddTransient<IPublisherService, PublisherService>();
                context.Services.AddTransient<INamingConventionService, AngularNamingConventionService>();
                context.Services.AddTransient<IDomainScriptGenerationService, DomainScriptGenerationService>();
                context.Services.AddTransient<ITenantScriptGenerationService, TenantScriptGenerationService>();
            }

            context.Services.AddScoped<IPathsService, DefaultPathsService>();
            context.Services.AddScoped<EnvironmentAccessor>();

            context.Services.AddTransient<ICustomTextService, CustomTextService>();
            context.Services.AddTransient<IDataService, DbDataService>();
            context.Services.AddTransient<IEnvironmentsService, EnvironmentService>();
            context.Services.AddTransient<ILayoutsService, NullLayoutsService>();
            context.Services.AddTransient<ILocalizationService, LocalizationService>();
            context.Services.AddTransient<IMigrationService, MigrationService>();
            context.Services.AddTransient<IModulesService, ModulesService>();
            context.Services.AddTransient<IMoldsterLookupService, MoldsterLookupService>();
            context.Services.AddTransient<IMoldsterService, MoldsterService>();
            context.Services.AddTransient<IMoldsterService, MoldsterService>();
            context.Services.AddTransient<IPageCategoryHtmlService, PageCategoryHtmlService>();
            context.Services.AddTransient<IPageCategoryParameterDomainService, PageCategoryParameterDomainService>();
            context.Services.AddTransient<IPageCategoryScriptGenerationService, PageCategoryScriptGenerationService>();
            context.Services.AddTransient<IPageCategoryService, PageCategoryService>();
            context.Services.AddTransient<IPageControlDataService, PageControlDataService>();
            context.Services.AddTransient<IPageEntityService, PageEntityService>();
            context.Services.AddTransient<IPageHtmlGenerationService, PageHtmlGenerationService>();
            context.Services.AddTransient<IPageParameterDataService, PageParameterDataService>();
            context.Services.AddTransient<IPageScriptGenerationService, PageScriptGenerationService>();
            context.Services.AddTransient<IPagesDataService, PagesDataService>();
            context.Services.AddTransient<IPreviewService, PreviewService>();
            context.Services.AddTransient<IPublisherHttpService, PublisherHttpService>();
            context.Services.AddTransient<IResourceScriptGenerationService, ResourceScriptGenerationService>();
            context.Services.AddTransient<IScriptModelMappingService, ScriptModelMappingService>();
            context.Services.AddTransient<ITenantService, TenantsService>();
            context.Services.AddTransient<IViewsService, DefaultViewsService>();

            context.Services.AddAutoMapper(typeof(MoldsterApplicationModule).Assembly);

            
            context.Services.AddServiceFor<Domain, DomainService>();
            context.Services.AddServiceFor<NavigationGroup, NavigationGroupService>();
            context.Services.AddServiceFor<NavigationGroup, NavigationGroupService>();
            context.Services.AddServiceFor<PageControl, PageControlService>();
            context.Services.AddTransient<IAuthorizationService, UIAuthorizationService>();
            context.Services.AddTransient<ICacheProvider, MemoryCacheProvider>();
            context.Services.AddTransient<IPagesDataService, PagesDataService>();
            context.Services.AddTransient<ITenantService, TenantsService>();
            context.Services.AddTransient<IUserDataService, UIUserDataService>();
            context.Services.AddTransient<IUserDataService, UserDataService>();
            context.Services.AddTransient<MoldsterLookupService>();

        }


    }
}
