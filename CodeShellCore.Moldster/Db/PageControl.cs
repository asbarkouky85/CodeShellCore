using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class PageControl : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public PageControl()
        {
            PageControlValidators = new HashSet<PageControlValidator>();
        }

        public long Id { get; set; }
        public long ControlId { get; set; }
        public long PageId { get; set; }
        public byte Accessability { get; set; }
        public long? SourceCollectionId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        [ForeignKey("ControlId")]
        [InverseProperty("PageControls")]
        public Control Control { get; set; }
        [ForeignKey("PageId")]
        [InverseProperty("PageControls")]
        public Page Page { get; set; }
        [ForeignKey("SourceCollectionId")]
        [InverseProperty("PageControls")]
        public DomainEntityCollection SourceCollection { get; set; }
        [InverseProperty("PageControl")]
        public ICollection<PageControlValidator> PageControlValidators { get; set; }
    }
}
