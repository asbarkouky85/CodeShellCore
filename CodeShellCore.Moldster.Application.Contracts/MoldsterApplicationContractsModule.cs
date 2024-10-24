using CodeShellCore.Modularity;
using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore;

namespace CodeShellCore.Moldster
{
    [DependsOn(
        typeof(CodeShellApplicationContractsModule)
        )]
    public class MoldsterApplicationContractsModule : CodeShellModule
    {
    }
}
