using System.Reflection;

using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

using CodeShellCore.DependencyInjection;
using CodeShellCore.Web.Razor.Elements;
using CodeShellCore.Web.Razor.Tables;
using CodeShellCore.Web.Razor.Services;

using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Services.Internal;

using CodeShellCore.Moldster.Razor.Services;
using CodeShellCore.Web.Razor.Elements.Angular;
using CodeShellCore.Web.Razor.Themes;
using CodeShellCore.Web.Razor.Validation.Internal;
using CodeShellCore.Web.Razor.Text;
using CodeShellCore.Web.Razor.General;
using CodeShellCore.Web.Razor.Elements.Moldster;
using CodeShellCore.Web.Razor.General.Moldster;
using CodeShellCore.Web.Razor.Tables.Angular;
using CodeShellCore.Web.Razor.Tables.Moldster;
using CodeShellCore.Moldster.Configurator;
using CodeShellCore.Moldster.Configurator.Services;
using CodeShellCore.CLI;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Web.Razor.SignalR;
using Microsoft.AspNetCore.Builder;
using CodeShellCore.Moldster.Services.Json;
using CodeShellCore.Moldster.Services.Db;
using CodeShellCore.Http.Pushing;
using CodeShellCore.Web.Razor.Configurator;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using CodeShellCore.Text.Localization;

namespace CodeShellCore.Web.Razor
{
    public static class DependencyExtensions
    {
        /// <summary>
        /// adds /generationHub and opens cross origin requests for all
        /// </summary>
        /// <param name="app"></param>
        public static void UseMoldsterServerGeneration(this IApplicationBuilder app)
        {

            app.UseCors(d => d.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowCredentials());
            app.UseSignalR(d =>
            {
                d.MapHub<GenerationHub>("/generationHub");
                d.MapHub<TasksHub>("/tasksHub");
            });
        }

        public static void AddMoldsterServerGeneration(this IServiceCollection coll, MoldsType t = MoldsType.Db)
        {
            coll.AddMoldsterCodeGeneration(t);
            coll.AddTransient<IDbViewsService, ServerViewsService>();
            coll.AddTransient<IViewsService, ServerViewsService>();
            coll.AddTransient<IOutputWriter, MessagePusherOutputService>();
            coll.AddTransient<IPushingSessionManager, ConfigSessionManager>();
            coll.AddTransient<ISessionManager, ConfigSessionManager>();

            coll.AddSignalR();
            coll.AddSignalRHub<IOutputMessageSender, GenerationHub>();
            coll.AddSignalRHub<IBundlingTasksNotifications, TasksHub>();
        }

        public static void AddMoldsterConfiguratorControllers(this IMvcBuilder coll)
        {
            var ass = typeof(DomainsController).Assembly;

            coll.AddApplicationPart(ass)
                .AddControllersAsServices()
                .ConfigureApplicationPartManager(d =>
            {
                d.FeatureProviders.Add(new ConfiguratorFeatureProvider());
            });
            SearchExpressions.RegisterExpressions();
        }

        public static void AddMoldsterWeb(this IServiceCollection coll, MoldsType t)
        {
            coll.AddMoldsterDbData();

            coll.AddServiceFor<Domain, DomainService>();
            coll.AddServiceFor<Page, PagesService>();
            coll.AddServiceFor<PageCategory, PageCategoryService>();
            coll.AddTransient<ServerViewsService>();

            coll.AddTransient<IRazorRenderingService, RazorRenderingService>();

            coll.AddSingleton<PathProvider>();
            coll.AddSingleton<IMoldProvider, DefaultMoldProvider>();

            coll.AddTransient<ILocalizationService, LocalizationService>();
            coll.AddTransient<IMoldsterService, MoldsterService>();

            switch (t)
            {
                case MoldsType.Json:
                    coll.AddTransient<IJsonConfigProvider, JsonDataService>();
                    coll.AddTransient<IDataService, JsonDataService>();

                    break;
                case MoldsType.Db:
                    coll.AddTransient<IDataService, DbDataService>();
                    coll.AddTransient<IMoldsterRazorService, MoldsterRazorService>();
                    break;
            }

        }

        public static void AddMvcRazorHelpers(this IServiceCollection coll)
        {
            coll.AddSingleton<IRazorLocaleTextProvider, MvcTextProvider>();
            coll.AddScoped<IElementsHelper, DefaultElementsHelper>();
            coll.AddScoped<ITablesHelper, DefaultTablesHelper>();
            coll.AddScoped<IGeneralHelper, DefaultGeneralHelper>();
        }

        public static void AddAngularRazorHelpers(this IServiceCollection coll)
        {
            coll.AddSingleton<IRazorLocaleTextProvider, AngularTextProvider>();
            coll.AddScoped<IElementsHelper, AngularElementsHelper>();
            coll.AddScoped<IAngularElementsHelper, AngularElementsHelper>();
            coll.AddScoped<ITablesHelper, AngularTablesHelper>();
            coll.AddScoped<IAngularTablesHelper, AngularTablesHelper>();
            coll.AddScoped<IGeneralHelper, DefaultGeneralHelper>();
        }

        public static void AddMoldsterRazorHelpers(this IServiceCollection coll)
        {
            coll.AddSingleton<IRazorLocaleTextProvider, AngularTextProvider>();

            coll.AddScoped<IElementsHelper, MoldsterElementsHelper>();
            coll.AddScoped<ITablesHelper, MoldsterTableHelper>();
            coll.AddScoped<IGeneralHelper, MoldsterGeneralHelper>();

            coll.AddScoped<IAngularElementsHelper, MoldsterElementsHelper>();
            coll.AddScoped<IAngularTablesHelper, MoldsterTableHelper>();

            coll.AddScoped<IMoldsterGeneralHelper, MoldsterGeneralHelper>();
            coll.AddScoped<IMoldsterTableHelper, MoldsterTableHelper>();
        }

        public static void ConfigureAngular2Razor(this Shell shell, IRazorTheme theme = null)
        {
            theme = theme ?? new AngularTheme();

            RazorConfig.SetCollectionType<AngularValidationCollection>();

            RazorConfig.FieldErrorMessagesTemplate = "<span *ngIf=\"{0}.controls['{1}'] && {0}.controls['{1}'].invalid && ({0}.controls['{1}'].dirty || {0}.controls['{1}'].touched)\">\r{2}</span>";
            RazorConfig.ErrorMessageTemplate = "<small *ngIf=\"{0}.controls['{1}'].errors!.{2}\" class=\"form-text text-danger\">{3}</small>\r";
            RazorConfig.LocaleTextProvider = new AngularTextProvider();
            RazorConfig.ExpressionStringifier = new AngularExpressionStringifier();

            RazorConfig.Theme = theme;
        }

        public static void ConfigureMvcRazor(this Shell shell, IRazorTheme theme = null)
        {
            theme = theme ?? new MvcTheme();

            RazorConfig.SetCollectionType<ValidationCollection>();

            RazorConfig.FieldErrorMessagesTemplate = "<span>\r{2}</span>";
            RazorConfig.ErrorMessageTemplate = "<small class=\"form-text text-danger\">{3}</small>\r";
            RazorConfig.LocaleTextProvider = new MvcTextProvider(new Language());
            RazorConfig.ExpressionStringifier = new DefaultExpressionStringifier();

            RazorConfig.Theme = theme;
        }

        public static void AddCodeShellEmbeddedViews(this IServiceCollection coll)
        {
            coll.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Add(new EmbeddedFileProvider(Assembly.Load("CodeShellCore.Web.Razor")));
            });
        }
    }
}
