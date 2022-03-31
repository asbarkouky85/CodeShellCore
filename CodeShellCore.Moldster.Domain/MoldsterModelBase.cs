using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodeShellCore.Moldster
{
    public class MoldsterModelBase : IEditable
    {
        [NotMapped]
        public string State { get; set; }
    }
}
