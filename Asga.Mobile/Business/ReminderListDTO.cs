using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Asga.Mobile.Business
{
    public class ReminderListDTO : NotificationListDTO
    {
        public DateTime ReminderTime { get; set; }
        public long UserId { get; set; }
        public static Expression<Func<UserReminder, ReminderListDTO>> ReminderExpression
        {
            get
            {
                return d => new ReminderListDTO
                {
                    Id = d.Id,
                    TextId = d.TextId,
                    EntityId = d.EntityId ?? 0,
                    EntityType = d.EntityType,
                    CreatedOn = d.CreatedOn,
                    title = d.Title,
                    body = d.Text,
                    ReminderTime = d.ReminderTime,
                    UserId = d.UserId
                };
            }
        }
    }
}
