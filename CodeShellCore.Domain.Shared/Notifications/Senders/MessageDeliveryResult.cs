using System.Collections.Generic;

namespace CodeShellCore.Notifications.Senders
{
    public class MessageDeliveryResult : MessageDeliveryResultBase
    {
        public List<DeviceMessageDeliveryResult> DeviceResults { get; private set; } = new List<DeviceMessageDeliveryResult>();
        public MessageDeliveryResult() { }

        public MessageDeliveryResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
