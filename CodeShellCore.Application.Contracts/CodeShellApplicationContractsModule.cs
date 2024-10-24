using CodeShellCore.Modularity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore
{
    [DependsOn(
        typeof(CodeShellDomainSharedModule)
        )]
    public class CodeShellApplicationContractsModule : CodeShellModule
    {
    }
}
