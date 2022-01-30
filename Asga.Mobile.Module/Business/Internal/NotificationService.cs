using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Linq;
using CodeShellCore.Data.Helpers;

namespace Asga.Mobile.Business.Internal
{

    public class NotificationService : MobileEntityService<Notification>, INotificationService
    {
        private readonly IAsgaMobileUnit _unit;
        public NotificationService(IAsgaMobileUnit unit) : base(unit)
        {
            _unit = unit;
        }

        public int GetCount(bool byUser = true)
        {
            var userId = _unit.UserAccessor.User?.GetUserIdAsLong();
            return Unit.UserNotificationRepository.Count(e => e.UserId == userId && e.IsRead == false);
        }

        public virtual LoadResult<NotificationListDTO> LoadListDTO(ListOptions<NotificationListDTO> opts, bool byUser = true)
        {
            var userId = _unit.UserAccessor.User?.GetUserIdAsLong();
            if (opts.OrderProperty == null)
            {
                opts.OrderProperty = "CreatedOn";
                opts.Direction = SortDir.DESC;
            }
            LoadResult<NotificationListDTO> data = new LoadResult<NotificationListDTO>();
            if (byUser)
            {
                data = Repository.FindAs(NotificationListDTO.Expression, opts, d => d.UserNotifications.Any(e => e.UserId == userId));
            }
            else
            {
                data = Repository.FindAs(NotificationListDTO.Expression, opts);
            }

            _unit.ServiceProvider.GetService<IModuleNotificationService>().FillAutoData(data.ListT);
            return data;
        }

        public SubmitResult SetRead(long id)
        {
            var userId = _unit.UserAccessor.User?.GetUserIdAsLong();
            var un= Unit.UserNotificationRepository.FindSingle(e => e.UserId == userId && e.NotificationId == id);
            if (un != null)
            {
                un.IsRead = true;
                Unit.UserNotificationRepository.Update(un);
            }
            return Unit.SaveChanges();
        }
    }
}
