using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class Validator : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public Validator()
        {
            ControlValidators = new HashSet<ControlValidator>();
            PageControlValidators = new HashSet<PageControlValidator>();
        }

        public long Id { get; set; }
        [StringLength(150)]
        public string Type { get; set; }
        [StringLength(50)]
        public string CalendarType { get; set; }
        [StringLength(50)]
        public string MinValue { get; set; }
        [StringLength(50)]
        public string MaxValue { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        [StringLength(150)]
        public string Pattern { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        [InverseProperty("Validator")]
        public ICollection<ControlValidator> ControlValidators { get; set; }
        [InverseProperty("Validator")]
        public ICollection<PageControlValidator> PageControlValidators { get; set; }
    }
}
