namespace CodeShellCore.Notifications
{
    public class NotifyAttachmentData
    {
        public string FileName { get; set; }
        public long? AttachmentId { get; set; }
        public string Url { get; set; }
        public string Base64 { get; set; }
    }
}