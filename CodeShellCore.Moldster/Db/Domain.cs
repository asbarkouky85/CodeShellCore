﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class Domain : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public Domain()
        {
            DomainEntities = new HashSet<DomainEntity>();
            PageCategories = new HashSet<PageCategory>();
            Pages = new HashSet<Page>();
        }

        public long Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? ParentId { get; set; }
        public string Chain { get; set; }
        public string NameChain { get; set; }

        [InverseProperty("Domain")]
        public ICollection<DomainEntity> DomainEntities { get; set; }
        [InverseProperty("Domain")]
        public ICollection<PageCategory> PageCategories { get; set; }
        [InverseProperty("Domain")]
        public ICollection<Page> Pages { get; set; }
    }
}
