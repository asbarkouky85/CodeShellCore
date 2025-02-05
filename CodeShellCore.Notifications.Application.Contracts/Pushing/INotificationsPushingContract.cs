using System.Threading.Tasks;

namespace CodeShellCore.Notifications.Pushing
{
    public interface INotificationsPushingContract
    {
        Task NotificationsChanged(int count);
    }
}
