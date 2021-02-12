using CodeShellCore.Data;
using CodeShellCore.Data.Auditing;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public.Business
{
    public class MessageListDTO : IDTO<Message>, IModel<long>, IChangeColumns
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string MessageTypeName { get; set; }
        public int BaseTypeId { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long StatusId { get; set; }
        public string StatusName => StatusId.ToEnumString<MessageStatus>();
        public long? ReferenceId { get; set; }
        public string ReferenceEntity { get; set; }
        public long? CreatedBy {get; set; }
        public long? UpdatedBy {get; set; }
        public DateTime? CreatedOn {get; set; }
        public DateTime? ResponseOn { get; set; }
        public DateTime? UpdatedOn {get; set; }
        
    }
}
