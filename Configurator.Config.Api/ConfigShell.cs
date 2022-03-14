using System.Globalization;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Web.Razor;
using CodeShellCore.Moldster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Security.Authorization;
using System;
using CodeShellCore.Moldster.Builder.Dtos;
using CodeShellCore.Moldster.Builder.Services;
using CodeShellCore.Moldster.Domains.Dtos;
using CodeShellCore.Moldster.Pages.Dtos;
using CodeShellCore.Moldster.Sql.Dtos;
using CodeShellCore.Moldster.Builder;

namespace Configurator.Config.Api
{
    public class ConfigShell : MoldsterWebShell
    {
        protected override bool MigrateOnStartup => true;
        protected override bool UseSwagger => true;

        public ConfigShell(IConfiguration config) : base(config)
        {
        }

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);

            
            coll.AddScriptMapping("Core/Example/Enumerations", d =>
            {
                string st = "";
                st += d.MapEnum<PageParameterTypes>();
                st += d.MapEnum<ParameterRequestTypes>();
                st += d.MapEnum<TextTypes>();
                return st;
            });

            coll.AddScriptMapping("Core/Example/Dtos", d =>
            {
                string st = "";
                st += d.MapEntity<PageParameterDTO>(true);
                st += d.MapEntity<RenderDTO>();
                st += d.MapEntity<DbCreationRequest>();
                st += d.MapEntity<ParameterRequestDTO>();
                st += d.MapEntity<BundlingTask>();
                st += d.MapEntity<PreviewData>();
                st += d.MapEntity<CustomTextRequest>();
                return st;
            });
        }

        

        public override void Dispose()
        {
            var ser = RootInjector.GetService<IPreviewService>();
            ser.StopPreview();
            base.Dispose();
        }
    }
}
