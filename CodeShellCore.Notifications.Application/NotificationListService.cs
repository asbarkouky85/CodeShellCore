using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.Text;
using CodeShellCore.Notifications.Senders;

namespace CodeShellCore.Notifications
{
    public class NotificationListService : ApplicationService, INotificationsListService
    {
        INotificationsUnit Unit => Store.GetService<INotificationsUnit>();
        INotificationDeliveryService Deliver => Store.GetService<INotificationDeliveryService>();
        IListNotificationSender listNotificationSender => Store.GetService<IListNotificationSender>();
        public NotificationListService(IServiceProvider provider) : base(provider)
        {
        }

        public async Task<SubmitResult> ChangeStatus(NotificationReadStatusDto dto)
        {
            Notification note = await Unit.NotificationRepository.FindSingle(dto.Id);
            if (note != null)
            {
                note.IsRead = dto.Status;
                Unit.NotificationRepository.Update(note);
                if (note.IsRead && note.EntityId != null)
                {
                    var nots = await Unit.NotificationRepository.Find(d => d.EntityId == note.EntityId && d.UserId == note.UserId);
                    foreach (var n in nots)
                    {
                        n.IsRead = true;
                        Unit.NotificationRepository.Update(n);
                    }
                }
            }
            var res = await Unit.SaveChanges();
            if (res.IsSuccess)
            {
                await listNotificationSender.SendCount(new NotificationCountSendDto { UserIds = new() { note.UserId } });
            }
            return res;
        }

        public Task<int> CountByUser()
        {
            return Unit.NotificationRepository.Count(d => d.UserId == UserAccessor.User.GetUserIdAsLong() && !d.IsRead);
        }

        public async Task<PagedResult<NotificationListDto>> GetByUser(PagedListRequestDto opts)
        {
            var notifications = await Unit.NotificationRepository.GetByUser(UserAccessor.User.GetUserIdAsLong() ?? 0, Mapper.Map(opts, new PagedListRequest()));
            var types = notifications.List.Select(e => e.NotificationTypeId).ToList();

            var templates = await Unit.NotificationTypeRepository.GetWithTemplates(types);
            var dtoList = new List<NotificationListDto>();
            var result = new PagedResult<NotificationListDto>
            {
                TotalCount = notifications.TotalCount,
            };

            foreach (var notification in notifications.List)
            {
                var type = templates.FirstOrDefault(e => e.Id == notification.NotificationTypeId);
                var templateContent = type.GetTemplate(NotificationProviders.List, Language.Culture.TwoLetterISOLanguageName);
                var dto = Mapper.Map(notification, new NotificationListDto());
                dto.Body = templateContent.ReplaceParameters(Unit.Strings, notification.Parameters);
                dtoList.Add(dto);
            }

            result.List = dtoList;
            return result;
        }

        
        public async Task Test()
        {
            await listNotificationSender.SendCount(new NotificationCountSendDto { UserIds = new() { 1 } });
        }
    }
}
