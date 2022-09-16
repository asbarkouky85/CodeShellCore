using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.PageCategories
{
    public class ControlDto : EntityDto<long>
    {
        public string Identifier { get; set; }
        public string ControlType { get; set; }
    }
}
