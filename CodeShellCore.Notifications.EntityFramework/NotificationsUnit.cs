using CodeShellCore.Data;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Notifications.Devices;
using CodeShellCore.Notifications.Users;
using System;

namespace CodeShellCore.Notifications
{
    public class NotificationsUnit : UnitOfWork<NotificationsContext>, INotificationsUnit
    {
        public NotificationsUnit(IServiceProvider userAccessor) : base(userAccessor)
        {
        }
        public INotificationRepository NotificationRepository => GetRepository<INotificationRepository>();

        public IRepository<User> UserRepository => GetRepositoryFor<User>();

        public IUserDeviceRepository UserDeviceRepository => GetRepository<IUserDeviceRepository>();

        public INotificationTypeRepository NotificationTypeRepository => GetRepository<INotificationTypeRepository>();
    }
}
