using CodeShellCore.Data;

namespace CodeShellCore.FileServer
{
    public partial class AttachmentBinary : IEntity<long>
    {
        public byte[] Bytes { get; private set; }

        public virtual Attachment Attachment { get; set; }
        public long Id { get; set; }

        protected AttachmentBinary()
        {

        }

        public AttachmentBinary(byte[] data)
        {
            Bytes = data;
        }
    }
}
