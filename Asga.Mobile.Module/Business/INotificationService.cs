using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;

namespace Asga.Mobile.Business
{
    public interface INotificationService : IEntityService<Notification>
    {
        LoadResult<NotificationListDTO> LoadListDTO(ListOptions<NotificationListDTO> opts, bool byUser = true);
        int GetCount(bool byUser=true);
        SubmitResult SetRead(long id);
    }
}