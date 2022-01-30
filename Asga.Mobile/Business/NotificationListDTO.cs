using CodeShellCore.Http.Pushing;
using System;
using System.Linq.Expressions;

namespace Asga.Mobile.Business
{
    public class NotificationListDTO : FirebaseMessage
    {
        public long Id { get; set; }
        public string TextId { get; set; }
        public long? EntityId { get; set; }
        public string EntityType { get; set; }
        public DateTime? CreatedOn { get; set; }


        public static Expression<Func<Notification, NotificationListDTO>> Expression
        {
            get
            {
                return e => new NotificationListDTO
                {
                    body = e.Text,
                    CreatedOn = e.CreatedOn,
                    EntityId = e.EntityId,
                    EntityType = e.EntityType,
                    TextId = e.TextId,
                    Id = e.Id
                };
            }
        }
    }
}
