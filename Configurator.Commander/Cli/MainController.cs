using System.Collections.Generic;
using System.Drawing;
using CodeShellCore.Cli;
using CodeShellCore.Moldster.Cli;
using CodeShellCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Moldster.Services;

namespace Configurator.Commander.Cli
{
    public class MainController : ConsoleController
    {
        public override Dictionary<int, string> Functions => new Dictionary<int, string>
        {
            { 1,"Views"},
            { 2,"Webpack"},
            { 3,"Localization"},
            { 4,"Builder"},
            { 5,"SQL"},
            { 6,"Test"}
        };

        public void Views()
        {
            var con = new ModulesConsoleController();
            con.Run();
        }

        public void Webpack()
        {
            var con = new WebpackConsoleController();
            con.Run();
        }

        public void Localization()
        {
            var con = new LocalizationConsoleController();
            con.Run();
        }

        public void Builder()
        {
            var con = new BuilderConsoleController();
            con.Run();
        }

        public void SQL()
        {
            var con = new SqlConsoleController();
            con.Run();
        }

        public void Test()
        {
            var p = GetService<IPreviewService>();
            if (p.CurrentPreview == null)
            {
                p.StartPreview("ASGA", "dev_server");
            }
            else
            {
                p.StopPreview();
            }
            
        }
    }
}
