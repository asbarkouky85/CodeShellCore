using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;

namespace Asga.Mobile.Business
{
    public interface IUserDeviceService : IEntityService<UserDevice>
    {
        SubmitResult UpdateUserDevice(string userIdString, DeviceType type, string deviceId, string connectionId, string lang);
        SubmitResult ClearDeviceConnection(string userId, string deviceId);
    }
}