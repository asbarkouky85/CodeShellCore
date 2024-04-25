using CodeShellCore.Data.EntityFramework;
using CodeShellCore.EntityFramework;
using CodeShellCore.Notifications.Devices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Notifications
{
    public class CodeShellNotificationsUnit<TDbContext> : UnitOfWork<TDbContext>, ICodeShellNotificationsUnit
        where TDbContext : CodeShellDbContext<TDbContext>, IDevicesDbContext
    {
        public CodeShellNotificationsUnit(IServiceProvider provider) : base(provider)
        {
        }

        public IUserDeviceRepository UserDeviceRepository => GetRepository<IUserDeviceRepository>();
    }
}
