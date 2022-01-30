using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Asga.Public
{

    public partial class Message
    {
        [NotMapped]
        public string StatusName => StatusId.ToEnumString<MessageStatus>();
        [NotMapped]
        public string CommentTypeName { get; set; }

        [NotMapped]
        public int? BaseType { get; set; }

        [NotMapped]
        public string BaseTypeName => BaseType?.ToEnumString<MessageTypes>();
        [NotMapped]
        public IEnumerable<Attachment> Attachments { get; set; }
    }
}
