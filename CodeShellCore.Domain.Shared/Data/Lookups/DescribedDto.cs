using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Lookups
{
    public class DescribedDto<TPrime> : Named<TPrime>
    {
        public string Description { get; set; }
    }
}
