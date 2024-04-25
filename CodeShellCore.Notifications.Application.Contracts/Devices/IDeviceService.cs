using System.Threading.Tasks;

namespace CodeShellCore.Notifications.Devices
{
    public interface IDeviceService
    {
        Task UpdateSignalRConnectionData(SignalRDeviceDataDto data);
        Task ClearSingalRConnectionData(SignalRDeviceDataDto data);
    }
}
