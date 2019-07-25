using CodeShellCore.Cli;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Moldster;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleProject.Commander
{
    public class CommanderShell : ConsoleShell
    {
        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);

            coll.AddMoldsterCli(MoldsType.Db);
        }
    }
}
