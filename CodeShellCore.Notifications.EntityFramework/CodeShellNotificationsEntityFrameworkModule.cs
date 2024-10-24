using Codeshell.Abp.EntityFrameworkCore.Devices;
using CodeShellCore.Modularity;
using CodeShellCore.Notifications.Devices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications
{
    [DependsOn(
        typeof(CodeShellNotificationsDomainModule))]
    public class CodeShellNotificationsEntityFrameworkModule : CodeShellModule
    {

    }
}
