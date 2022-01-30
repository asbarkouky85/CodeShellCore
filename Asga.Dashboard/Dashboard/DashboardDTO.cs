using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Dashboard
{
    public class DashboardDTO : IEmplement
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ListName { get; set; }
        public IEnumerable<string> Labels { get; set; }
        public decimal Total { get; set; }
        public bool Translate { get; set; }
        public string BtnName { get; set; }
        public bool HasHeader { get; set; } = true;
        public string ImageName { get; set; }
        public List<DashboardListMember> Data { get; set; }
    }
}
