using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asga.Public
{
    [Table("Messages", Schema = "Publ")]
    public partial class Message : AsgaPublicModelBase, IAsgaPublicModel
    {
        public long Id { get; set; }
        [StringLength(150)]
        public string Title { get; set; }
        [StringLength(300)]
        public string Body { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(11)]
        public string Phone { get; set; }
        public int StatusId { get; set; }
        public long MessageTypeId { get; set; }
        [StringLength(150)]
        public string Response { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ResponseOn { get; set; }
        public long? UserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public long? ReferenceId { get; set; }
        [StringLength(100)]
        public string ReferenceEntity { get; set; }

        [ForeignKey("MessageTypeId")]
        [InverseProperty("Messages")]
		[System.Runtime.Serialization.IgnoreDataMember]
        public MessageType MessageType { get; set; }
    }
}
