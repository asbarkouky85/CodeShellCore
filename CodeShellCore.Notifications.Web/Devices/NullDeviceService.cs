using System.Threading.Tasks;

namespace CodeShellCore.Notifications.Devices
{
    public class NullDeviceService : IDeviceService
    {
        public Task ClearSingalRConnectionData(SignalRDeviceDataDto data)
        {
            return Task.CompletedTask;
        }

        public Task UpdateSignalRConnectionData(SignalRDeviceDataDto data)
        {
            return Task.CompletedTask;
        }
    }
}
