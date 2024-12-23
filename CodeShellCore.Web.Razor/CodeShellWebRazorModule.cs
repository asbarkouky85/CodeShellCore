using CodeShellCore.Http.Pushing;
using CodeShellCore.Modularity;
using CodeShellCore.Moldster;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Web.Razor.Elements;
using CodeShellCore.Web.Razor.Elements.Angular;
using CodeShellCore.Web.Razor.General;
using CodeShellCore.Web.Razor.Services;
using CodeShellCore.Web.Razor.Tables;
using CodeShellCore.Web.Razor.Tables.Angular;
using CodeShellCore.Web.Razor.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Web.Razor
{
    [DependsOn(
        typeof(CodeShellWebModule),
        typeof(MoldsterApplicationContractsModule))]
    public class CodeShellWebRazorModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            var legacy = context.Configuration.GetSection("Moldster:Legacy").Get<bool>();
            if (legacy)
            {
                context.Services.AddTransient<IViewsService, LegacyRazorViewsService>();
            }
            else
            {
                context.Services.AddTransient<IViewsService, RazorViewsService>();
            }

            context.Services.AddTransient<IMoldsterRazorRenderingService, MoldsterRazorRenderingService>();

            context.Services.AddTransient<IPushingSessionManager, ConfigSessionManager>();
            context.Services.AddTransient<ISessionManager, ConfigSessionManager>();
            context.Services.AddTransient<ILayoutsService, RazorPathsProvider>();

            context.Services.AddSingleton<IRazorLocaleTextProvider, AngularTextProvider>();
            context.Services.AddScoped<IElementsHelper, AngularElementsHelper>();
            context.Services.AddScoped<IAngularElementsHelper, AngularElementsHelper>();
            context.Services.AddScoped<ITablesHelper, AngularTablesHelper>();
            context.Services.AddScoped<IAngularTablesHelper, AngularTablesHelper>();
            context.Services.AddScoped<IGeneralHelper, DefaultGeneralHelper>();

            context.Services.Configure<CodeShellAppOptions>(opt =>
            {
                opt.DefaultCulture = "en-US";
                opt.UseLocalization = false;
            });

            context.Services.Configure<CodeShellWebAppOptions>(e =>
            {
                e.UseSwagger = true;
                e.UseHealthCheck = true;
                e.UseCors = true;
                e.DefaultCorsOrigins = "http://localhost:8050,http://localhost:4200,http://127.0.0.1:8050,http://127.0.0.1:8051";
            });

            context.Services.AddMoldsterRazorHelpers();
            context.Services.AddRazorPages().AddRazorRuntimeCompilation();


        }


        public override void OnApplicationStarted(CodeShellApplicationInitializationContext context)
        {
            RazorConfig.UseAngular2Razor();
        }
    }
}

