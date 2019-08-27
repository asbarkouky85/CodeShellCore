using CodeShellCore.Cli;
using CodeShellCore.Moldster.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Cli
{
    public class BuilderConsoleController : ConsoleController
    {
        IMoldsterService Moldster { get { return GetService<IMoldsterService>(); } }
        IScriptGenerationService Scripts { get { return GetService<IScriptGenerationService>(); } }

        public override Dictionary<int, string> Functions => new Dictionary<int, string>
        {
            { 1, "Init"},
            { 2, "AddStaticFiles"},
        };

        public void Init()
        {
            Scripts.GenerateEnvironment();

        }

        public void AddStaticFiles()
        {
            Moldster.AddStaticFiles();
        }
    }
}
