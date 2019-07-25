﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class ControlValidator : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public long Id { get; set; }
        public long ControlId { get; set; }
        public long ValidatorId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        [ForeignKey("ControlId")]
        [InverseProperty("ControlValidators")]
        public Control Control { get; set; }
        [ForeignKey("ValidatorId")]
        [InverseProperty("ControlValidators")]
        public Validator Validator { get; set; }
    }
}
