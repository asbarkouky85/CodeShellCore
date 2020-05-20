using System;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Data.Services;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Services.Internal;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Moldster.Db.Internal;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Moldster.Db.Data.Internal;
using CodeShellCore.Services;
using CodeShellCore.Moldster.Configurator.Services;
using CodeShellCore.Moldster.Services.Db;
using CodeShellCore.Moldster.Services.Json;
using CodeShellCore.Moldster.Definitions;

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

        public static void AddMoldsterModules(this IServiceCollection coll,Action<MoldsterModulesConfig> modules) 
        {
           
            var conf = new MoldsterModulesConfig();
            modules(conf);
        }

        public static void AddMoldsterConfigurator(this IServiceCollection coll, MoldsType t)
        {
            coll.AddMoldsterCodeGeneration(t);
            coll.AddTransient<WriterService>();

            switch (t)
            {
                case MoldsType.Db:
                    coll.AddMoldsterDbData();
                    coll.AddTransient<ConfiguratorLookupService>();
                    coll.AddServiceFor<Domain, DomainService>();
                    coll.AddServiceFor<PageCategory, ConfigPageCategoryService>();
                    coll.AddServiceFor<Page, PagesService>();
                    coll.AddServiceFor<NavigationGroup, NavigationGroupService>();
                    coll.AddServiceFor<PageControl, PageControlService>();
                    coll.AddServiceFor<NavigationGroup, NavigationGroupService>();
                    coll.AddServiceFor<Tenant, TenantsService>();
                    break;

            }


        }

        public static void AddMoldsterCodeGeneration(this IServiceCollection coll, MoldsType t)
        {
            coll.AddSingleton<IMoldProvider, DefaultMoldProvider>();

            coll.AddScoped<IPathsService, PathProvider>();
            coll.AddScoped<EnvironmentAccessor>();

            coll.AddTransient<WriterService>();
            coll.AddTransient<ILocalizationService, LocalizationService>();
            coll.AddTransient<IMoldsterService, MoldsterService>();
            coll.AddTransient<IScriptModelMappingService, ScriptModelMappingService>();
            coll.AddTransient<IPublisherService, PublisherService>();
            coll.AddTransient<IPublisherHttpService, PublisherHttpService>();
            coll.AddTransient<ISqlCommandService, SqlCommandService>();
            coll.AddTransient<IBundlingService, BundlingService>();
            coll.AddTransient<IPreviewService, PreviewService>();
            coll.AddTransient<IAppFileHandler, AppFileHandler>();
            coll.AddTransient<IModulesService, ModulesService>();

            switch (t)
            {
                case MoldsType.Json:
                    coll.AddTransient<IDataService, JsonDataService>();
                    coll.AddTransient<IJsonConfigProvider, JsonDataService>();
                    coll.AddTransient<IScriptGenerationService, JsonTsGeneratorService>();
                    coll.AddTransient<ITemplateProcessingService, JsonTemplateProcessingService>();
                    coll.AddTransient<IViewsService, DefaultViewsService>();
                    break;
                case MoldsType.Db:
                    coll.AddMoldsterDbData();
                    coll.AddTransient(typeof(IEntityService<>), typeof(EntityService<>));
                    coll.AddTransient<IDataService, DbDataService>();

                    coll.AddTransient<IPageControlDataService, PageControlDataService>();
                    coll.AddTransient<IPageParameterDataService, PageParameterDataService>();
                    coll.AddTransient<ITemplateDataService, TemplateDataService>();
                    coll.AddTransient<IPagesDataService, PagesService>();
                    coll.AddTransient<ITenantService, TenantsService>();
                    
                    coll.AddTransient<IViewsService, DbViewsHttpService>();
                    coll.AddTransient<IMoldsterService, DbMoldsterService>();
                    coll.AddTransient<IScriptGenerationService, DbTsGeneratorService>();
                    coll.AddTransient<ITemplateProcessingService, DbTemplateProcessingService>();
                    coll.AddTransient<IDbViewsService, DbViewsHttpService>();
                    coll.AddTransient<IDbTemplateProcessingService, DbTemplateProcessingService>();
                    break;

            }
        }

        public static void AddMoldsterCli(this IServiceCollection coll, MoldsType t)
        {
            coll.AddMoldsterCodeGeneration(t);
        }
    }
}
