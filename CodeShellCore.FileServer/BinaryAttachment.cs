using CodeShellCore.Data;

namespace CodeShellCore.FileServer
{
    public partial class BinaryAttachment : IModel<long>
    {
        public byte[] Bytes { get; private set; }

        public virtual Attachment Attachment { get; set; }
        public long Id { get; set; }

        protected BinaryAttachment()
        {

        }

        public BinaryAttachment(byte[] data)
        {
            Bytes = data;
        }
    }
}
