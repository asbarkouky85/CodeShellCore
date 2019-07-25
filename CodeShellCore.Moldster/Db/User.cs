﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class User : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public long Id { get; set; }
        [StringLength(150)]
        public string LogonName { get; set; }
        [StringLength(150)]
        public string DisplayName { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
