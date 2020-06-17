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
        IBundlingService Scripts { get { return GetService<IBundlingService>(); } }

        public override Dictionary<int, string> Functions => new Dictionary<int, string>
        {
            { 1, "Init"},
            { 2, "AddSQLFiles"},
            { 3, "AddStaticFiles"},
            { 4, "Add_core_codeshell"},
        };

        public void Init()
        {
            Scripts.GenerateEnvironment();

        }

        public void AddSQLFiles()
        {
            Scripts.AddSQLFiles();
        }

        public void AddStaticFiles()
        {
            Scripts.AddStaticFiles();
        }

        public void Add_core_codeshell()
        {
            Scripts.AddCodeShell();
        }
    }
}
