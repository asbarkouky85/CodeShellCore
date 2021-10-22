using System.Globalization;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Web.Razor;
using CodeShellCore.Moldster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Builder;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Security.Authorization;
using System;

namespace Configurator.Config.Api
{
    public class ConfigShell : MoldsterWebShell
    {
        protected override bool MigrateOnStartup => false;
        
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
