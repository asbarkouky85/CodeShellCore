using System.Collections.Generic;

using CodeShellCore.Cli;
using CodeShellCore.Moldster.Cli;

namespace ExampleProject.Commander.Cli
{
    public class MainController : ConsoleController
    {
        public override Dictionary<int, string> Functions => new Dictionary<int, string>
        {
            { 1,"Modules"},
            { 2,"Webpack"},
            { 3,"Localization"}
        };

        public void Modules()
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
    }
}
