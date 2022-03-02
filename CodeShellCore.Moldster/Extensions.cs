using System;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Data.Services;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Services.Internal;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Configurator.Services;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.Builder.Internal;
using CodeShellCore.Moldster.Builder;
using CodeShellCore.Moldster.CodeGeneration.Internal;
using CodeShellCore.Moldster.Data.Internal;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Localization.Internal;
using CodeShellCore.Moldster.Data.Repositories;
using CodeShellCore.Moldster.Data.Repositories.Internal;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Razor.Internal;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Caching;

namespace CodeShellCore.Moldster
{
    public enum MoldsType { Json, Db }
    public static class Extensions
    {

        public static void AddMoldsterDbData(this IServiceCollection coll)
        {
            coll.AddContext<MoldsterContext>();
            coll.AddUnitOfWork<ConfigUnit, IConfigUnit>();
            coll.AddGenericRepository(typeof(MoldsterRepository<,>));

            coll.AddRepositoryFor<CustomText, CustomTextRepository, ICustomTextRepository>();
            coll.AddRepositoryFor<PageControl, PageControlRepository, IPageControlRepository>();
            coll.AddRepositoryFor<PageCategory, PageCategoryRepository, IPageCategoryRepository>();
            coll.AddRepositoryFor<Domain, DomainRepository, IDomainRepository>();
            coll.AddRepositoryFor<Resource, ResourceRepository, IMoldsterResourceRepository>();
            coll.AddRepositoryFor<Page, PageRepository, IPageRepository>();
            coll.AddRepositoryFor<NavigationGroup, NavigationGroupRepository, INavigationGroupRepository>();
            coll.AddRepositoryFor<NavigationPage, NavigationPageRepository, INavigationPageRepository>();
            coll.AddRepositoryFor<PageCategoryParameter, PageCategoryParameterRepository, IPageCategoryParameterRepository>();
            coll.AddRepositoryFor<PageParameter, PageParameterRepository, IPageParameterRepository>();
            coll.AddRepositoryFor<PageRoute, PageRouteRepository, IPageRouteRepository>();
        }

        public static void UseEnumerationMapping(this IServiceCollection coll, string toPath, Func<IScriptModelMappingService, string> f)
        {
            var mapp = new ScriptMapping(f)
            {
                FilePath = toPath
            };
            ScriptMapSettings.Add(mapp);
        }

        public static void AddScriptMapping(this IServiceCollection coll, string toPath, Func<IScriptModelMappingService, string> f)
        {
            var mapp = new ScriptMapping(f)
            {
                FilePath = toPath
            };
            ScriptMapSettings.Add(mapp);
        }

        public static void AddMoldsterModules(this IServiceCollection coll, Action<MoldsterModulesConfig> modules)
        {

            var conf = new MoldsterModulesConfig();
            modules(conf);
        }

        public static void AddMoldsterConfigurator(this IServiceCollection coll)
        {
            coll.AddMoldsterCodeGeneration();

            coll.AddMoldsterDbData();
            coll.AddTransient<ConfiguratorLookupService>();

            
            coll.AddServiceFor<Domain, DomainService>();
            coll.AddServiceFor<PageCategory, PageCategoryService>();
            coll.AddServiceFor<Page, PagesService>();
            coll.AddServiceFor<NavigationGroup, NavigationGroupService>();
            coll.AddServiceFor<PageControl, PageControlService>();
            coll.AddServiceFor<NavigationGroup, NavigationGroupService>();
            coll.AddServiceFor<Tenant, TenantsService>();

            coll.AddTransient<IUserDataService, UserDataService>();
            coll.AddTransient<ICacheProvider, CodeShellCore.Caching.MemoryCacheProvider>(); 
        }

        public static void AddMoldsterCodeGeneration(this IServiceCollection coll)
        {
            coll.AddSingleton<IMoldProvider, DefaultMoldProvider>();

            coll.AddScoped<IPathsService, DefaultPathsService>();
            coll.AddScoped<EnvironmentAccessor>();

            coll.AddTransient<ILocalizationService, LocalizationService>();
            coll.AddTransient<ICustomTextService, CustomTextService>();
            coll.AddTransient<IMoldsterService, MoldsterService>();
            coll.AddTransient<IScriptModelMappingService, ScriptModelMappingService>();
            coll.AddTransient<IPublisherService, PublisherService>();
            coll.AddTransient<IPublisherHttpService, PublisherHttpService>();
            coll.AddTransient<ISqlCommandService, SqlCommandService>();
            coll.AddTransient<IBundlingService, BundlingService>();
            coll.AddTransient<IPreviewService, PreviewService>();
            coll.AddTransient<IModulesService, ModulesService>();

            coll.AddMoldsterDbData();
            coll.AddTransient(typeof(IEntityService<>), typeof(EntityService<>));
            coll.AddTransient<IDataService, DbDataService>();

            coll.AddTransient<IPageControlDataService, PageControlDataService>();
            coll.AddTransient<IPageParameterDataService, PageParameterDataService>();
            coll.AddTransient<ITemplateDataService, TemplateDataService>();
            coll.AddTransient<IPagesDataService, PagesService>();
            coll.AddTransient<ITenantService, TenantsService>();

            coll.AddTransient<IViewsService, DefaultViewsService>();
            coll.AddTransient<IMoldsterService, MoldsterService>();
            coll.AddTransient<IScriptGenerationService, ScriptGenerationService>();
            coll.AddTransient<ITemplateProcessingService, TemplateProcessingService>();
        }

        public static void AddMoldsterCli(this IServiceCollection coll)
        {
            coll.AddMoldsterCodeGeneration();
        }
    }
}
