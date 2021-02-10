using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Services.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Configurator
{
    public interface IOutputMessageSender : IPushingContract
    {
        Task SendMessage(NotificationDTO notificationDTO);  //string type, string payload
    }
}
