using CodeShellCore.Data.Auditing;
using CodeShellCore.Data;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Text;
using System;using System.Collections.Generic;using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;
using CodeShellCore.Notifications.Users;
using CodeShellCore.Notifications.Types;
namespace CodeShellCore.Notifications{    public partial class Notification : AuditedEntity<long>, IEditable    {        public bool IsRead { get; set; }        public NotificationType NotificationType { get; set; }        public long NotificationTypeId { get; set; }
        public ICollection<NotificationMessage> NotificationMessages { get; set; }
        public ICollection<NotificationAttachment> NotificationAttachments { get; set; }
        public string Parameters { get; set; }        public long UserId { get; set; }        [StringLength(300)]        public string Link { get; set; }        [StringLength(70)]        public string EntityType { get; set; }        public long? EntityId { get; set; }        public string SenderName { get; set; }        public long? SenderId { get; set; }        public bool IsSent { get; set; }        [StringLength(150)]        public string Subject { get; set; }        [Column(TypeName = "datetime")]        public DateTime? ReadOn { get; set; }        [ForeignKey("UserId")]        [InverseProperty("Notifications")]
        [System.Runtime.Serialization.IgnoreDataMember]
        public User User { get; set; }

        [NotMapped]
        public string State { get; set; }

        public Notification() : base()
        {
            Id = Utils.GenerateID();
            NotificationMessages = new HashSet<NotificationMessage>();
        }        public Notification(long messageId, string entityType = null, long? entityId = null, object data = null) : this()
        {
            NotificationTypeId = messageId;
            EntityType = entityType;
            EntityId = entityId;
            IsRead = false;
            IsSent = false;
            Parameters = data?.ToJson();
        }

        public void SetUser(long? userId, string userName)
        {
            SenderId = userId;
            SenderName = userName;
        }

        public Notification Clone()
        {
            var not = new Notification();
            not.AppendProperties(this);
            not.Id = Utils.GenerateID();
            return not;
        }

        public void SetProviders(List<NotificationProviders> providers)
        {
            foreach (var provider in providers)
            {
                NotificationMessages.Add(new NotificationMessage(provider));
            }
        }
    }
}