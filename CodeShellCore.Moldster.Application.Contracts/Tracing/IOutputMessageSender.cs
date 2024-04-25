using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Tracing
{
    public interface IOutputMessageSender
    {
        Task SendMessage(NotificationDTO notificationDTO);  //string type, string payload
    }
}
