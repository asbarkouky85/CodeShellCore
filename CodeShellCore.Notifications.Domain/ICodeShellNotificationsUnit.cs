using CodeShellCore.Data;
using CodeShellCore.Notifications.Devices;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Notifications
{
    public interface ICodeShellNotificationsUnit : IUnitOfWork
    {
        IUserDeviceRepository UserDeviceRepository { get; }
    }
}
