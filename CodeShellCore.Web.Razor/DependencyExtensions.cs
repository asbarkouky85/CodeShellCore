using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

using CodeShellCore.DependencyInjection;
using CodeShellCore.Cli;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Http.Pushing;
using CodeShellCore.Text.Localization;

using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Razor.Services;
using CodeShellCore.Moldster.Razor;

using CodeShellCore.Web.Razor.Elements;
using CodeShellCore.Web.Razor.Tables;
using CodeShellCore.Web.Razor.Services;
using CodeShellCore.Web.Razor.Elements.Angular;
using CodeShellCore.Web.Razor.Themes;
using CodeShellCore.Web.Razor.Validation.Internal;
using CodeShellCore.Web.Razor.Text;
using CodeShellCore.Web.Razor.General;
using CodeShellCore.Web.Razor.Elements.Moldster;
using CodeShellCore.Web.Razor.General.Moldster;
using CodeShellCore.Web.Razor.Tables.Angular;
using CodeShellCore.Web.Razor.Tables.Moldster;
using CodeShellCore.Web.Razor.SignalR;
using CodeShellCore.Web.Razor.Controllers.Configurator;
using Microsoft.AspNetCore.Routing;
using CodeShellCore.Moldster.Pages.Services;
using CodeShellCore.Moldster.PageCategories.Services;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Localization.Services;
using CodeShellCore.Moldster.Domains.Services;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Tracing;
using CodeShellCore.Moldster.Builder;

namespace CodeShellCore.Web.Razor
{
    public static class DependencyExtensions
    {

        public static void AddMoldsterServerGeneration(this IServiceCollection coll)
        {
            coll.AddMoldsterCodeGeneration();

            coll.AddTransient<IViewsService, ServerViewsService>();
            coll.AddScoped<IOutputWriter, MessagePusherOutputService>();
            coll.AddTransient<IPushingSessionManager, ConfigSessionManager>();
            coll.AddTransient<ISessionManager, ConfigSessionManager>();
            coll.AddTransient<IPathsService, RazorPathsProvider>();

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

        public static void AddMoldsterWeb(this IServiceCollection coll)
        {

            coll.AddServiceFor<Domain, DomainService>();
            coll.AddServiceFor<Page, PagesService>();
            coll.AddServiceFor<PageCategory, PageCategoryService>();
            coll.AddTransient<ServerViewsService>();

            coll.AddTransient<IRazorRenderingService, RazorRenderingService>();

            coll.AddSingleton<DefaultPathsService>();
            coll.AddSingleton<IMoldProvider, AngularNg11MoldProvider>();

            coll.AddTransient<ILocalizationService, LocalizationService>();
            coll.AddTransient<IMoldsterService, MoldsterService>();

            coll.AddTransient<IDataService, DbDataService>();
            coll.AddTransient<IMoldsterRazorService, MoldsterRazorService>();
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

        public static void AddAbpCustomization(this IServiceCollection coll)
        {
            coll.AddSingleton<IRazorLocaleTextProvider, AbpTextProvider>();
            coll.AddTransient<ILocalizationService, AbpLocalizationService>();
        }


        public static void AddMoldsterHubs(this IEndpointRouteBuilder builder)
        {
            builder.MapHub<GenerationHub>("/generationHub");
            builder.MapHub<TasksHub>("/tasksHub");
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

        public static void ConfigureAngularApbRazor(this Shell shell, IRazorTheme theme = null)
        {
            theme = theme ?? new AbpLeptonTheme();

            RazorConfig.SetCollectionType<AngularValidationCollection>();

            RazorConfig.LocaleTextProvider = new AbpTextProvider();
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
    }
}
