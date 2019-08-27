using System.Reflection;

using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

using CodeShellCore.DependencyInjection;
using CodeShellCore.Text.Localization;
using CodeShellCore.Text.TextProviders;
using CodeShellCore.Web.Razor.Elements;
using CodeShellCore.Web.Razor.Tables;
using CodeShellCore.Web.Razor.Services;

using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Moldster.Db.Services;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Services.Internal;
using CodeShellCore.Moldster.Json;
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

namespace CodeShellCore.Web.Razor
{
    public static class DependencyExtensions
    {
        public static void AddMoldsterWeb(this IServiceCollection coll, MoldsType t)
        {
            coll.AddMoldsterDbData();

            coll.AddServiceFor<Domain, DomainService>();
            coll.AddServiceFor<Page, PagesService>();
            coll.AddServiceFor<Tenant, TenantsService>();
            coll.AddServiceFor<PageCategory, PageCategoryService>();
            coll.AddServiceFor<PageControl, PageControlsService>();

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

        public static void AddAngularRazorHelpers(this IServiceCollection coll)
        {
            coll.AddSingleton<ILocaleTextProvider, AngularTextProvider>();
            coll.AddScoped<IElementsHelper, AngularElementsHelper>();
            coll.AddScoped<IAngularElementsHelper, AngularElementsHelper>();
            coll.AddScoped<ITablesHelper, AngularTablesHelper>();
            coll.AddScoped<IAngularTablesHelper, AngularTablesHelper>();
            coll.AddScoped<IGeneralHelper, DefaultGeneralHelper>();
        }

        public static void AddMoldsterRazorHelpers(this IServiceCollection coll)
        {
            coll.AddSingleton<ILocaleTextProvider, AngularTextProvider>();
            coll.AddScoped<IElementsHelper, MoldsterElementsHelper>();
            coll.AddScoped<IAngularElementsHelper, MoldsterElementsHelper>();
            coll.AddScoped<ITablesHelper, MoldsterTableHelper>();
            coll.AddScoped<IMoldsterTableHelper, MoldsterTableHelper>();
            coll.AddScoped<IGeneralHelper, MoldsterGeneralHelper>();
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

        public static void AddCodeShellEmbeddedViews(this IServiceCollection coll)
        {
            coll.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Add(new EmbeddedFileProvider(Assembly.Load("CodeShellCore.Web.Razor")));
            });
        }
    }
}
