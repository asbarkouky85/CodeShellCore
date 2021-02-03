using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodeShellCore.Moldster
{
    public partial class Control
    {
        [NotMapped]
        public ResourceCollection Collection { get; set; }
        
    }
}
