using Asga.Mobile.Data;
using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Mobile
{
    public interface IAsgaMobileUnit : IUnitOfWork
    {
        IRepository<UserReminder> UserReminderRepository { get; }
        IRepository<Notification> NotificationRepository { get; }
        IRepository<UserDevice> UserDeviceRepository { get; }
        IUserNotificationRepository UserNotificationRepository { get; }
    }
}
