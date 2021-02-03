using CodeShellCore.Data.Helpers;
using CodeShellCore.Http.Pushing;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeShellCore.Services;
using CodeShellCore;

namespace Asga.Mobile.Business.Internal
{
    public abstract class ModuleNotificationService : StandAloneService, IModuleNotificationService
    {
        protected IAsgaMobileUnit MUnit => GetService<IAsgaMobileUnit>();
        protected ILocaleTextProvider Translator => GetService<ILocaleTextProvider>();
        public ModuleNotificationService(IServiceProvider provider) : base(provider)
        {
        }

        public abstract IEnumerable<NotificationParameters> GetParameters(string messageId, IEnumerable<long> ids);
        public abstract IEnumerable<long> GetUsersByEventId(string eventId, long? entityId = null);
        public abstract string GetEntityByEvent(string eventId);
        public virtual bool UseSignalR => false;
        public virtual bool UseFirebase => false;

        public virtual void FillNoteData(FirebaseMessage mess, string messageId, NotificationParameters pars, string lang = null)
        {
            if (lang == null)
            {
                mess.body = Translator.Message(messageId, pars.BodyParams.ToArray());
                mess.title = Translator.Message(messageId + "_title", pars.TitleParams.ToArray());
            }
            else
            {
                mess.body = Translator.MessageWithCulture(messageId, lang, pars.BodyParams.ToArray());
                mess.title = Translator.MessageWithCulture(messageId + "_title", lang, pars.TitleParams.ToArray());
            }

            mess.image = pars.Image;
        }

        public virtual void FillAutoData(IEnumerable<NotificationListDTO> lst, string lang = null)
        {

            var types = lst.Where(d => d.TextId != null).Select(d => d.TextId).Distinct();

            List<NotificationParameters> parList = new List<NotificationParameters>();

            foreach (var t in types)
            {
                var ids = lst.Where(d => d.TextId == t && d.EntityId != null).Select(d => d.EntityId.Value).ToList();
                var pars = GetParameters(t, ids);
                parList.AddRange(pars);
            }

            foreach (var v in lst)
            {
                var data = parList.FirstOrDefault(d => d.EntityId == v.EntityId);
                if (data != null)
                    FillNoteData(v, v.TextId, data);
            }
        }

        public virtual SubmitResult AddEventNotification(string eventId, long? entityId = null)
        {
            var n = new Notification
            {
                EntityId = entityId,
                EntityType = entityId == null ? null : GetEntityByEvent(eventId),
                TextId = eventId,
            };
            var users = GetUsersByEventId(eventId,entityId);
            if (users.Any())
            {
                foreach (var u in users)
                {
                    n.UserNotifications.Add(new UserNotification { UserId = u, IsRead = false });
                }
            }
            MUnit.NotificationRepository.Add(n);
            var res = MUnit.SaveChanges();
            if (res.IsSuccess)
            {
                SendSignalR(n);
            }
            return res;
        }

        protected virtual void SendFireBase(Notification n)
        {
            var users = n.UserNotifications.Select(e => e.UserId).ToList();

            if (UseFirebase && users.Any())
            {

                var tokens = MUnit.UserDeviceRepository.FindAs(e => new { e.TokenId, e.UserId, e.PreferredLanguage }, e => users.Contains(e.UserId) && e.Type == (int)DeviceType.Firebase);

                if (!tokens.Any())
                    return;

                var lngs = tokens.Where(e => e.PreferredLanguage != null).Select(e => e.PreferredLanguage).Distinct().ToList();
                if (!lngs.Any())
                    lngs.Add(Shell.DefaultCulture.TwoLetterISOLanguageName);

                Dictionary<string, NotificationListDTO> trns = new Dictionary<string, NotificationListDTO>();

                foreach (var l in lngs)
                {
                    var dto = NotificationListDTO.Expression.Compile().Invoke(n);
                    var lst = new[] { dto };
                    FillAutoData(new[] { dto }, l);
                    trns[l] = lst[0];
                }
                var ser = GetService<PushNotificationService>();
                foreach (var u in users)
                {
                    var ts = tokens.Where(e => e.UserId == u).Select(e => new { e.TokenId, e.PreferredLanguage }).ToArray();
                    foreach (var t in ts)
                    {
                        if (trns.TryGetValue(t.PreferredLanguage, out NotificationListDTO dto))
                        {
                            ser.SendNotification(dto, t.TokenId);
                        }
                    }
                }
            }
        }

        protected virtual void SendSignalR(Notification n)
        {

            var users = n.UserNotifications.Select(e => e.UserId).ToList();

            if (UseSignalR && users.Any())
            {
                var ser = GetService<IMessagePusher<INotificationsPushingContract>>();
                var tokens = MUnit.UserDeviceRepository.FindAs(e => new { e.TokenId, e.UserId }, e => users.Contains(e.UserId) && e.Type == (int)DeviceType.SignalR);
                var counts = MUnit.UserNotificationRepository.CountByUsers(users, e => !e.IsRead);

                foreach (var u in users)
                {
                    var c = counts.Where(e => e.UserId == u).Select(e => e.Count).FirstOrDefault();
                    var t = tokens.Where(e => e.UserId == u).Select(e => e.TokenId).ToArray();
                    if (c > 0 && t.Any())
                    {
                        ser.Publish(e => e.NotificationsChanged(c), t);
                    }
                }
            }


        }
    }
}
