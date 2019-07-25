using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class PageCategory : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public PageCategory()
        {
            Controls = new HashSet<Control>();
            Pages = new HashSet<Page>();
        }

        public long Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public long? DomainEntityId { get; set; }
        [StringLength(300)]
        public string BaseComponent { get; set; }
        [StringLength(300)]
        public string ViewPath { get; set; }
        public string Layout { get; set; }
        [StringLength(300)]
        public string ScriptPath { get; set; }
        public long? ResourceId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        [ForeignKey("DomainEntityId")]
        [InverseProperty("PageCategories")]
        public DomainEntity DomainEntity { get; set; }
        [ForeignKey("ResourceId")]
        [InverseProperty("PageCategories")]
        public Resource Resource { get; set; }
        [InverseProperty("PageCategory")]
        public ICollection<Control> Controls { get; set; }
        [InverseProperty("PageCategory")]
        public ICollection<Page> Pages { get; set; }
    }
}
