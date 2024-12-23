using CodeShellCore.Modularity;
using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore;
using CodeShellCore.Web.Razor.Moldster;

namespace CodeShellCore.Moldster
{
    [DependsOn(
        typeof(CodeShellApplicationContractsModule)
        )]
    public class MoldsterApplicationContractsModule : CodeShellModule
    {
        public override void OnApplicationStarted(CodeShellApplicationInitializationContext context)
        {
            MoldsterSearchExpressions.RegisterExpressions();
        }
    }
}
