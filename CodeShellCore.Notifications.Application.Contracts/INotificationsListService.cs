using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications
{
    public interface INotificationsListService
    {
        Task<SubmitResult> ChangeStatus(NotificationReadStatusDto dto);
        Task<int> CountByUser();
        Task<PagedResult<NotificationListDto>> GetByUser(PagedListRequestDto opts);
        Task Test();
    }
}