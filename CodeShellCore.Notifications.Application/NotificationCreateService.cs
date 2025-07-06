using CodeShellCore.Notifications.Senders;
using CodeShellCore.Security;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications
{
    public class NotificationCreateService : ApplicationService, INotificationCreateService
    {
        INotificationsUnit Unit => Store.GetService<INotificationsUnit>();
        INotificationAttachmentStorage AttachmentStorage => Store.GetService<INotificationAttachmentStorage>();
        INotificationDeliveryService Delivery => Store.GetService<INotificationDeliveryService>();
        public NotificationCreateService(IServiceProvider provider) : base(provider)
        {
        }

        public async Task CreateNotifications(NotificationCreateRequestData request)
        {
            if (request.Users != null && request.Users.Any())
            {
                if (request.HasAttachments())
                    await _createAttachments(request);

                var type = await Unit.NotificationTypeRepository.GetWithProviders(request.NotificationTypeId);
                Notification notification = _createNotification(request.NotificationTypeId, request.EntityType, request.EntityId, request.Parameters);
                foreach (var notificationUser in request.Users)
                {
                    var userNote = notification.Clone();
                    userNote.UserId = notificationUser.UserId;

                    userNote.SetProviders(type.Providers.Select(e => e.NotificationProviderId).ToList());

                    if (notificationUser.HasAttachments())
                    {
                        foreach (var attachmentDto in notificationUser.Attachments)
                        {
                            var attachment = Mapper.Map(attachmentDto, new NotificationAttachment());
                            userNote.NotificationAttachments.Add(attachment);
                        }
                    }

                    if (request.HasAttachments())
                    {
                        foreach (var attachmentDto in request.Attachments)
                        {
                            var attachment = Mapper.Map(attachmentDto, new NotificationAttachment());
                            userNote.NotificationAttachments.Add(attachment);
                        }
                    }

                    Unit.NotificationRepository.Add(userNote);
                }
                var result = await Unit.SaveChangesAsync();
                if (result.IsSuccess)
                {
                    await Delivery.SendPendingMessages();
                }
            }
        }

        private async Task _createAttachments(NotificationCreateRequestData request)
        {
            foreach (var attachmentDto in request.Attachments)
            {
                await _storeFileIfBase64(attachmentDto);
                foreach (var user in request.Users)
                {
                    if (user.HasAttachments())
                    {
                        foreach (var attachment in user.Attachments)
                            await _storeFileIfBase64(attachment);
                    }
                }
            }
        }

        private async Task _storeFileIfBase64(NotifyAttachmentData attachmentDto)
        {

            if (attachmentDto.Base64 != null)
            {
                attachmentDto.AttachmentId = await AttachmentStorage.CreateFileFromBase64(attachmentDto.FileName, attachmentDto.Base64);
            }
        }

        private Notification _createNotification(long typeId, string entityType, long? entityId = null, object data = null)
        {
            Notification notification = new Notification(typeId, entityType, entityId, data);
            if (long.TryParse(UserAccessor.UserId, out long userId))
                notification.SetUser(userId, UserAccessor.User?.Name);
            return notification;
        }
    }
}
