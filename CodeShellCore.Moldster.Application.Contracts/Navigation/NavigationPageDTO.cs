using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Navigation
{
    public class NavigationPageDTO
    {
        public string RouteParameters { get; set; }
        public object ActionName { get; set; }
        public string PrivilegeType { get; set; }
        public string PageIdentifier { get; set; }
        public string ResourceName { get; set; }
        public string Apps { get; set; }
        public string Url { get; set; }
    }
}
