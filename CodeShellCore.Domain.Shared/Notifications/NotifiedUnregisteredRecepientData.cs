using System.Collections.Generic;

namespace CodeShellCore.Notifications
{
    public class NotifiedUnregisteredRecepientData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<NotifyAttachmentData> Attachments { get; set; }
    }
}