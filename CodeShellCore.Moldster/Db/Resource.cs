using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class Resource : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public Resource()
        {
            PageCategories = new HashSet<PageCategory>();
            Pages = new HashSet<Page>();
            ResourceActions = new HashSet<ResourceAction>();
        }

        public long Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        public long DomainId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        [ForeignKey("DomainId")]
        [InverseProperty("Resources")]
        public Domain Domain { get; set; }
        [InverseProperty("Resource")]
        public ICollection<PageCategory> PageCategories { get; set; }
        [InverseProperty("Resource")]
        public ICollection<Page> Pages { get; set; }
        [InverseProperty("Resource")]
        public ICollection<ResourceAction> ResourceActions { get; set; }
    }
}
