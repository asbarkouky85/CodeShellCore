using CodeShellCore.Caching;
using CodeShellCore.Data.Services;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Moldster.Builder;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Data.Repositories;
using CodeShellCore.Moldster.Data.Repositories.Internal;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Domains.Services;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.Environments.Services;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Localization.Services;
using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Moldster.Navigation.Services;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Pages.Services;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Resources;
using CodeShellCore.Moldster.Resources.Services;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Sql;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Security.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace CodeShellCore.Moldster
{
    public enum MoldsType { Json, Db }
    public static class Extensions
    {
        public static void AddMoldsterDbData(this IServiceCollection coll, IConfiguration config = null)
        {
            if (config != null)
            {
                var conn = config.GetConnectionString("Moldster") ?? config.GetConnectionString("Default");
                if (string.IsNullOrEmpty(conn))
                {
                    throw new Exception("Moldster Connection string is not found in appsettings");
                }
                else if (conn != "TEST")
                {
                    coll.AddDbContext<MoldsterContext>(e => e.UseSqlServer(conn));
                }
            }
            else
            {
                coll.AddDbContext<MoldsterContext>();
            }

            //coll.AddCodeShellEntityFramework();
            coll.AddUnitOfWork<ConfigUnit, IConfigUnit>();
            coll.AddCodeShellEntityFramework();
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

        public static void AddMoldsterConfigurator(this IServiceCollection coll, bool legacy = false)
        {
            coll.AddMoldsterCodeGeneration(legacy);

            coll.AddTransient<MoldsterLookupService>();

            coll.AddServiceFor<Domain, DomainService>();

            coll.AddTransient<IPagesDataService, PagesDataService>();
            coll.AddServiceFor<NavigationGroup, NavigationGroupService>();
            coll.AddServiceFor<PageControl, PageControlService>();
            coll.AddServiceFor<NavigationGroup, NavigationGroupService>();

            coll.AddTransient<ITenantService, TenantsService>();

            coll.AddTransient<IUserDataService, UserDataService>();
            coll.AddTransient<ICacheProvider, MemoryCacheProvider>();

            coll.AddLookupsService<MoldsterLookupService>();
        }

        public static void AddMoldsterCodeGeneration(this IServiceCollection coll, bool legacy = false)
        {
            if (legacy)
            {
                coll.AddSingleton<IMoldProvider, LegacyAngularMoldProvider>();
                coll.AddTransient<IBundlingService, LegacyBundlingService>();
                coll.AddTransient<ILegacyBundlingService, LegacyBundlingService>();
                coll.AddTransient<IInitializationService, LegacyInitializationService>();
                coll.AddTransient<IPublisherService, LegacyPublisherService>();
                coll.AddTransient<INamingConventionService, LegacyAngularNamingConventionService>();
                coll.AddTransient<IDomainScriptGenerationService, LegacyDomainScriptGenerationService>();
                coll.AddTransient<ITenantScriptGenerationService, LegacyTenantScriptGenerationService>();

            }
            else
            {
                coll.AddTransient<ILegacyBundlingService, BundlingService>();
                coll.AddSingleton<IMoldProvider, AngularMoldProvider>();
                coll.AddTransient<IBundlingService, BundlingService>();
                coll.AddTransient<IInitializationService, InitializationService>();
                coll.AddTransient<IPublisherService, PublisherService>();
                coll.AddTransient<INamingConventionService, AngularNamingConventionService>();
                coll.AddTransient<IDomainScriptGenerationService, DomainScriptGenerationService>();
                coll.AddTransient<ITenantScriptGenerationService, TenantScriptGenerationService>();
            }

            coll.AddScoped<IPathsService, DefaultPathsService>();
            coll.AddScoped<EnvironmentAccessor>();

            coll.AddTransient<ICustomTextService, CustomTextService>();
            coll.AddTransient<ILocalizationService, LocalizationService>();
            coll.AddTransient<IModulesService, ModulesService>();
            coll.AddTransient<IMoldsterService, MoldsterService>();
            coll.AddTransient<IMoldsterLookupService, MoldsterLookupService>();
            coll.AddTransient<IPreviewService, PreviewService>();
            coll.AddTransient<IPublisherHttpService, PublisherHttpService>();

            coll.AddTransient<IScriptModelMappingService, ScriptModelMappingService>();
            coll.AddTransient<ISqlCommandService, SqlCommandService>();

            coll.AddTransient<IMigrationService, MigrationService>();

            coll.AddTransient<IEnvironmentsService, EnvironmentService>();

            coll.AddTransient(typeof(IEntityService<>), typeof(EntityService<>));
            coll.AddTransient<IDataService, DbDataService>();

            coll.AddTransient<IPageControlDataService, PageControlDataService>();
            coll.AddTransient<IPageParameterDataService, PageParameterDataService>();
            coll.AddTransient<IPageCategoryParameterService, PageCategoryParameterService>();
            coll.AddTransient<IPagesDataService, PagesDataService>();
            coll.AddTransient<ITenantService, TenantsService>();
            coll.AddTransient<IPageCategoryService, PageCategoryService>();
            coll.AddTransient<IPageEntityService, PageEntityService>();

            coll.AddTransient<IViewsService, DefaultViewsService>();
            coll.AddTransient<IMoldsterService, MoldsterService>();

            coll.AddTransient<IPageCategoryScriptGenerationService, PageCategoryScriptGenerationService>();
            coll.AddTransient<IPageScriptGenerationService, PageScriptGenerationService>();
            coll.AddTransient<IResourceScriptGenerationService, ResourceScriptGenerationService>();
            coll.AddTransient<IPageCategoryHtmlService, PageCategoryHtmlService>();
            coll.AddTransient<IPageHtmlGenerationService, PageHtmlGenerationService>();
            coll.AddAutoMapper(Assembly.GetCallingAssembly());
        }

        public static void AddMoldsterCli(this IServiceCollection coll, bool legacy = false)
        {
            coll.AddMoldsterCodeGeneration(legacy);
        }


    }
}
