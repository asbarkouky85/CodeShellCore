namespace CodeShellCore.Notifications.Senders
{
    public class UserMessageDeliveryData
    {
        public long? UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
    }
}