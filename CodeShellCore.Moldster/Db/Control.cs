﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class Control : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public Control()
        {
            ControlValidators = new HashSet<ControlValidator>();
            InverseParentControlNavigation = new HashSet<Control>();
            PageControls = new HashSet<PageControl>();
        }

        public long Id { get; set; }
        [StringLength(50)]
        public string ControlType { get; set; }
        [Column(TypeName = "ntext")]
        public string DesignParameters { get; set; }
        public long? ParentControl { get; set; }
        public long? PageCategoryId { get; set; }
        public long? DomainEntityPropertyId { get; set; }
        [StringLength(200)]
        public string Identifier { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        [ForeignKey("DomainEntityPropertyId")]
        [InverseProperty("Controls")]
        public DomainEntityProperty DomainEntityProperty { get; set; }
        [ForeignKey("PageCategoryId")]
        [InverseProperty("Controls")]
        public PageCategory PageCategory { get; set; }
        [ForeignKey("ParentControl")]
        [InverseProperty("InverseParentControlNavigation")]
        public Control ParentControlNavigation { get; set; }
        [InverseProperty("Control")]
        public ICollection<ControlValidator> ControlValidators { get; set; }
        [InverseProperty("ParentControlNavigation")]
        public ICollection<Control> InverseParentControlNavigation { get; set; }
        [InverseProperty("Control")]
        public ICollection<PageControl> PageControls { get; set; }
    }
}
