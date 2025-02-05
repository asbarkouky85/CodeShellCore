using CodeShellCore.Data;
using CodeShellCore.Notifications.Users;

namespace CodeShellCore.Notifications
{
    public interface INotificationsUnit : IUnitOfWork, ICodeShellNotificationsUnit
    {
        INotificationTypeRepository NotificationTypeRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IRepository<User> UserRepository { get; }
    }
}