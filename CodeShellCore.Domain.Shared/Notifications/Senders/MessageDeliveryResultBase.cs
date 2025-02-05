using System;

namespace CodeShellCore.Notifications.Senders
{
    public class MessageDeliveryResultBase
    {
        public bool IsSuccess { get; set; }
        public string RequestContent { get; set; }
        public string ResponseContent { get; set; }
        public string Exception { get; set; }
        public bool InvalidUserData { get; set; }

        public void SetException(Exception ex)
        {
            IsSuccess = false;
            Exception = ex.GetMessageAndStack();
        }
    }
}
