using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodeShellCore.Moldster.Db
{
    public partial class PageCategoryParameter
    {
        [NotMapped]
        public string TypeString { get { return ((PageParameterTypes)Type).StringFormat(); } }
    }
}
