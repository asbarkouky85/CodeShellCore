using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class Client : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public long Id { get; set; }
        [StringLength(100)]
        public string Identifier { get; set; }
        [StringLength(100)]
        public string Secret { get; set; }
        [StringLength(300)]
        public string Address { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
