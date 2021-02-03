using Asga.Mobile.Business;
using CodeShellCore.Security;
using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Mobile.Web.Conrollers
{
    [ApiAuthorize(AllowAll = true)]
    public class UserDevicesController : BaseApiController
    {
        IUserDeviceService service => GetService<IUserDeviceService>();
        IUserAccessor Acc => GetService<IUserAccessor>();
        Language lang => GetService<Language>();

        public virtual IActionResult UpdateDeviceToken([FromBody] DeviceData data)
        {
            if (Acc.IsUser)
            {
                SubmitResult = service.UpdateUserDevice(Acc.UserId, DeviceType.Firebase, data.DeviceId, data.Token, data.PreferredLanguage ?? lang.Culture.TwoLetterISOLanguageName);
            }
            return Respond();
        }
    }
}
