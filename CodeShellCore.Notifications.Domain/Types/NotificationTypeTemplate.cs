using CodeShellCore.Helpers;
using CodeShellCore.Notifications.Providers;
using CodeShellCore.Text;
using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CodeShellCore.Notifications.Types
{
    public class NotificationTypeTemplate : AuditedEntity<long>
    {
        public NotificationType NotificationType { get; set; }
        public long NotificationTypeId { get; set; }
        public string BodyTemplate { get; set; }
        public string Code { get; set; }
        public string TitleTemplate { get; set; }
        public string FieldsToUse { get; set; }
        public NotificationProviders? NotificationProviderId { get; set; }
        public NotificationProvider NotificationProvider { get; set; }
        public int? Lcid { get; set; }

        public NotificationTypeTemplate()
        {
            Id = Utils.GenerateID();
        }

        public NotificationTypeTemplate(string locale, string bodyTemplate, string titleTemplate) : this()
        {
            Lcid = new CultureInfo(locale).LCID;
            BodyTemplate = bodyTemplate;
            TitleTemplate = titleTemplate;
        }

        public NotificationTypeTemplate(string local, NotificationProviders provider, string code, string[] fieldsToUse = null) : this()
        {
            Lcid = new CultureInfo(local).LCID;
            NotificationProviderId = provider;
            Code = code;
            
            FieldsToUse = fieldsToUse?.ToJson();
        }

        private string _replace(string message, ILocaleTextProvider strings, string data)
        {
            if (!string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(message))
            {
                var dic = data.FromJson<Dictionary<string, object>>();
                foreach (var d in dic)
                {
                    var v = d.Value?.ToString();
                    if (!string.IsNullOrEmpty(v) && v.StartsWith("Words."))
                    {
                        v = strings.Word(v.GetAfterLast("."));
                    }
                    else if (!string.IsNullOrEmpty(v) && v.StartsWith("Message."))
                    {
                        v = strings.Message(v.GetAfterLast("."));
                    }
                    if (!string.IsNullOrEmpty(v) && v.StartsWith("Message."))
                    {
                        v = strings.Message(v.GetAfterLast("."));
                    }
                    message = message.Replace("{{" + d.Key + "}}", v);
                }
            }
            return message;
        }

        public string ReplaceParameters(ILocaleTextProvider strings, string data)
        {

            var message = BodyTemplate;
            message = _replace(message, strings, data);
            return message;

        }

        public string ReplaceTitleParameters(ILocaleTextProvider strings, string data)
        {
            var message = TitleTemplate;
            message = _replace(message, strings, data);
            return message;
        }
    }
}
