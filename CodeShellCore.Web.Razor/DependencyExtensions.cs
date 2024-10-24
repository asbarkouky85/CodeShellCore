using CodeShellCore.Cli;
using CodeShellCore.Http.Pushing;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Builder;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Tracing;
using CodeShellCore.Notifications;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Razor.Elements;
using CodeShellCore.Web.Razor.Elements.Angular;
using CodeShellCore.Web.Razor.Elements.Moldster;
using CodeShellCore.Web.Razor.General;
using CodeShellCore.Web.Razor.General.Moldster;
using CodeShellCore.Web.Razor.Services;
using CodeShellCore.Web.Razor.SignalR;
using CodeShellCore.Web.Razor.Tables;
using CodeShellCore.Web.Razor.Tables.Angular;
using CodeShellCore.Web.Razor.Tables.Moldster;
using CodeShellCore.Web.Razor.Text;
using CodeShellCore.Web.Razor.Themes;
using CodeShellCore.Web.Razor.Validation.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Web.Razor
{
    public static class DependencyExtensions
    {

        public static void AddMvcRazorHelpers(this IServiceCollection coll)
        {
            coll.AddSingleton<IRazorLocaleTextProvider, MvcTextProvider>();
            coll.AddScoped<IElementsHelper, DefaultElementsHelper>();
            coll.AddScoped<ITablesHelper, DefaultTablesHelper>();
            coll.AddScoped<IGeneralHelper, DefaultGeneralHelper>();
        }

        public static void AddAngularRazorHelpers(this IServiceCollection coll)
        {

        }


        public static void AddMoldsterHubs(this IEndpointRouteBuilder builder)
        {
            
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

        
    }
}
