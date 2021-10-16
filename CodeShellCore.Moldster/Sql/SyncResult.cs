using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Dto
{
    public class SyncResult
    {
        public string SourceTenant { get; set; }
        public string TargetTenant { get; set; }
        public int AddedPages { get; set; }
        public int UpdatedPages { get; set; }
        public int AddedPageControls { get; set; }
        public int UpdatedPageControls { get; set; }
        public int NavigationPages { get; set; }
    }
}
