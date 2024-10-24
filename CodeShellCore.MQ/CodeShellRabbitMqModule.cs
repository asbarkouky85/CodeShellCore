using CodeShellCore.Modularity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MQ
{

    [DependsOn(
        typeof(CodeShellModule)
    )]
    public class CodeShellRabbitMqModule : CodeShellModule
    {

    }
}
