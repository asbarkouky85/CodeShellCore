using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodeShellCore.Moldster.Db
{
    public partial class PageCategory
    {
        [NotMapped]
        public string ResourceName { get; set; }
    }
}
