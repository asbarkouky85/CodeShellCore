using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Mobile.Business.Internal
{
    public class UserDeviceService : EntityService<UserDevice>, IUserDeviceService
    {
        private readonly IAsgaMobileUnit unit;

        public UserDeviceService(IAsgaMobileUnit unit) : base(unit)
        {
            this.unit = unit;
        }

        public SubmitResult ClearDeviceConnection(string userIdString, string deviceId)
        {
            if (!long.TryParse(userIdString, out long userId))
                return new SubmitResult();
            unit.UserDeviceRepository.Delete(e => e.UserId == userId && e.DeviceId == deviceId);
            return unit.SaveChanges();
        }

        public SubmitResult UpdateUserDevice(string userIdString, DeviceType type, string deviceId, string connectionId, string lang)
        {
            if (!long.TryParse(userIdString, out long userId))
                return new SubmitResult();

            UserDevice u = unit.UserDeviceRepository.FindSingle(d => d.UserId.Equals(userId) && d.DeviceId == deviceId);
            if (u != null)
            {
                u.TokenId = connectionId ?? u.TokenId;
                u.PreferredLanguage = lang ?? u.PreferredLanguage;
                unit.UserDeviceRepository.Update(u);
            }
            else
            {
                u = new UserDevice
                {
                    UserId = userId,
                    DeviceId = deviceId,
                    TokenId = connectionId,
                    Type = (int)(unit.ClientData.IsMobile ? DeviceType.Firebase : DeviceType.SignalR)
                };
                unit.UserDeviceRepository.Add(u);
            }
            return unit.SaveChanges();
        }


    }
}
