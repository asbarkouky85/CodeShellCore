using CodeShellCore.Data.Lookups;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CodeShellCore.Notifications.Types
{
    public class NotificationType : AuditedEntity<long>, INamed<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<NotificationTypeNotificationProvider> Providers { get; set; }
        public ICollection<NotificationTypeTemplate> Templates { get; set; }

        public NotificationTypeTemplate GetTemplate(NotificationProviders providerId, string locale)
        {
            var lcid = new CultureInfo(locale).LCID;
            var template = Templates.FirstOrDefault(e => e.NotificationProviderId == providerId && e.Lcid == lcid);
            if (template == null)
                template = Templates.FirstOrDefault(e => e.NotificationProviderId == providerId && e.Lcid == null);
            if (template == null)
                template = Templates.FirstOrDefault(e => e.NotificationProviderId == null && e.Lcid == lcid);
            if (template == null)
                template = Templates.FirstOrDefault(e => e.NotificationProviderId == null && e.Lcid == null);
            return template;
        }

        public NotificationType()
        {
            Providers = new HashSet<NotificationTypeNotificationProvider>();
            Templates = new HashSet<NotificationTypeTemplate>();
        }

        public NotificationType(IEnumerable<NotificationProviders> providers) : this()
        {
            foreach (var provider in providers)
                Providers.Add(new NotificationTypeNotificationProvider(provider));
        }

        public void UpdateFrom(NotificationType entity)
        {
            Name = entity.Name;
            Code = entity.Code;
            Templates = entity.Templates;
            Providers = entity.Providers;
        }
    }
}
