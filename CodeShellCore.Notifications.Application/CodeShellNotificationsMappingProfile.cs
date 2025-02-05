using AutoMapper;
using CodeShellCore.Notifications.Devices;
using CodeShellCore.Notifications.Senders;
using CodeShellCore.Notifications.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications
{
    public class CodeShellNotificationsMappingProfile : Profile
    {
        public CodeShellNotificationsMappingProfile()
        {
            CreateMap<NotifyAttachmentData, NotificationAttachment>();
            CreateMap<Notification, NotificationListDto>()
                .ForMember(e => e.Resource, e => e.MapFrom(e => e.EntityType));
            CreateMap<User, UserMessageDeliveryData>()
                .ForMember(e => e.UserId, e => e.MapFrom(d => d.Id))
                .ForMember(e => e.PhoneNumber, e => e.MapFrom(d => d.Mobile))
                .ForMember(e => e.Name, e => e.MapFrom(d => d.Name));
            CreateMap<UserDevice, UserDeviceData>();
        }
    }
}
