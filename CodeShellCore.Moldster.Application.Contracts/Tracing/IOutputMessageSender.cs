using CodeShellCore.Services.Notifications;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Tracing
{
    public interface IOutputMessageSender : IPushingContract
    {
        Task SendMessage(NotificationDTO notificationDTO);  //string type, string payload
    }
}
