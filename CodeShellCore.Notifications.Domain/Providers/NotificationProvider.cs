using CodeShellCore.Data.Lookups;
using CodeShellCore.Notifications.Devices;
using CodeShellCore.Notifications.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Notifications.Providers
{
    public class NotificationProvider : FullAuditedEntity<NotificationProviders>, INamed<NotificationProviders>
    {
        public string Name { get; set; }
        public bool SendDirectly { get; set; }
        public bool EnableRetry { get; set; }
        public bool RequiresDevices { get; set; }
        public ICollection<NotificationTypeNotificationProvider> NotificationTypes { get; set; }
        public ICollection<NotificationTypeTemplate> Templates { get; set; }
        public ICollection<UserDevice> UserDevices { get; set; }

        private NotificationProvider()
        {
            NotificationTypes = new HashSet<NotificationTypeNotificationProvider>();
            Templates = new HashSet<NotificationTypeTemplate>();
            UserDevices = new HashSet<UserDevice>();

        }

        public NotificationProvider(NotificationProviders id, bool requiresDevices = false, string name = null) : this()
        {
            Id = id;
            Name = name ?? id.ToString();
            RequiresDevices = requiresDevices;
        }
    }
}
