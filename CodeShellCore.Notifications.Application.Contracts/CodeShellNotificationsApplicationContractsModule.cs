using CodeShellCore.Modularity;
using CodeShellCore.Notifications.Devices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Notifications
{
    [DependsOn(
        typeof(CodeShellApplicationContractsModule)
        )]
    public class CodeShellNotificationsApplicationContractsModule : CodeShellModule
    {

    }
}
