using CodeShellCore.Cli;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Moldster;
using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Moldster.Services;
using Configurator.Commander.Service;
using CodeShellCore;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Services.Internal;

namespace Configurator.Commander
{
    public class CommanderShell : ConsoleShell
    {
        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);

            coll.AddMoldsterCli(MoldsType.Db);

            coll.AddSingleton<IMoldProvider, ConfigMoldProvider>();
            coll.AddScriptMapping("Core/Example/Enumerations", d =>
            {
                string st = "";
                st += d.MapEnum<PageParameterTypes>();
                return st;
            });

            coll.AddScriptMapping("Core/Example/Dtos", d =>
            {
                string st = "";
                st += d.MapEntity<PageParameterDTO>(true);
                st += d.MapEntity<RenderDTO>();
                st += d.MapEntity<DbCreationRequest>();
                st += d.MapEntity<BundlingTask>();
                st += d.MapEntity<PreviewData>();
                return st;
            });
        }

        protected override void OnReady()
        {
            base.OnReady();
        }

        public override void Dispose()
        {
            
            var p= RootInjector.GetService<IPreviewService>();
            p.StopPreview();
            base.Dispose();
        }
    }
}
