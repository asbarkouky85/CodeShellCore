using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Navigation.Dtos
{
    public class ApplyOrderDTO
    {
        public long? SourceId { get; set; }
        public long? TargetId { get; set; }
        //public long? CurrentOrder { get; set; }
        //public long? TargetOrder { get; set; }
    }
}
