using CodeShellCore.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Mobile.Business
{
    public interface IModuleNotificationService
    {
        IEnumerable<NotificationParameters> GetParameters(string messageId,IEnumerable<long> ids);
        void FillAutoData(IEnumerable<NotificationListDTO> lst, string lang = null);
    }
}
