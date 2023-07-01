using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Navigation.Dtos
{
    public class NavigationPageDTO
    {
        public string RouteParameters { get; set; }
        public object ActionName { get; internal set; }
        public string PrivilegeType { get; internal set; }
        public string PageIdentifier { get; internal set; }
        public string ResourceName { get; internal set; }
        public string Apps { get; set; }
        public string Url { get; set; }
    }
}
