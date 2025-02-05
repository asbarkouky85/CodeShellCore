using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class NotificationsController : NotificationsBaseController, INotificationsListService
    {
        readonly INotificationsListService service;

        public NotificationsController(INotificationsListService service)
        {
            this.service = service;
        }

        [ApiAuthorize(AllowAnonymous = false)]
        public async Task<PagedResult<NotificationListDto>> GetByUser(PagedListRequestDto opts)
        {
            return await service.GetByUser(opts);

        }

        [HttpGet]
        [ApiAuthorize(AllowAnonymous = false)]
        public Task<int> CountByUser()
        {
            return service.CountByUser();
        }

        [HttpPut]
        [ApiAuthorize(AllowAnonymous = false)]
        public Task<SubmitResult> ChangeStatus(NotificationReadStatusDto dto)
        {
            return service.ChangeStatus(dto);
        }

        [HttpGet]
        [ApiAuthorize(AllowAnonymous = true)]
        public async Task Test()
        {
            await service.Test();
        }
    }
}
