using CodeShellCore.Moldster.Angular;
using CodeShellCore.Moldster.Db.Services;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Moldster.Json;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Services.Internal;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Moldster.Db.Internal;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Moldster.Db.Data.Internal;
using System;
using CodeShellCore.Data.Services;

namespace CodeShellCore.Moldster
{
    public enum MoldsType { Json, Db }
    public static class Extensions
    {

        public static void AddMoldsterDbData(this IServiceCollection coll)
        {
            coll.AddContext<ConfigurationContext>();
            coll.AddUnitOfWork<ConfigUnit, IConfigUnit>();
            coll.AddGenericRepository(typeof(MoldsterRepository<,>));

            coll.AddRepositoryFor<PageControl, PageControlRepository>();
            coll.AddRepositoryFor<TenantDomain, TenantDomainRepository>();
            coll.AddRepositoryFor<PageCategory, PageCategoryRepository>();
            coll.AddRepositoryFor<Resource, ResourceRepository>();
            coll.AddRepositoryFor<Page, PageRepository>();
        }

        public static void UseEnumerationMapping(this IServiceCollection coll, string toPath, Func<IScriptGenerationService, string> f)
        {
            var mapp = new MappedEnumerations(f)
            {
                FilePath = toPath
            };
            coll.AddSingleton<IMappedEnumerations>(mapp);
        }

        

        public static void AddMoldsterCli(this IServiceCollection coll, MoldsType t)
        {
            coll.AddSingleton<PathProvider>();
            coll.AddSingleton<IMoldProvider, DefaultMoldProvider>();
            coll.AddTransient<ILocalizationService, LocalizationService>();
            coll.AddTransient<IMoldsterService, MoldsterService>();
            coll.AddSingleton<IMappedEnumerations>(new MappedEnumerations(null));
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
                    coll.AddServiceFor<PageControl, PageControlsService>();
                    coll.AddServiceFor<Page, PagesService>();
                    coll.AddServiceFor<Domain, DomainService>();

                    coll.AddTransient<IDataService, DbDataService>();
                    coll.AddTransient<IScriptGenerationService, DbTsGeneratorService>();
                    coll.AddTransient<IDbViewsService, DbViewsHttpService>();
                    coll.AddTransient<IViewsService, DbViewsHttpService>();
                    coll.AddTransient<IMoldsterService, CustomizableViewsMoldsterService>();
                    coll.AddTransient<ITemplateProcessingService, DbTemplateProcessingService>();
                    coll.AddTransient<ICustomizablePagesService, DbTemplateProcessingService>();

                    break;

            }

        }
    }
}
