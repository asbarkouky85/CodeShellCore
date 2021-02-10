using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Data
{
    public class DashboardDTO : IEmplement
    {
        public long Id { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public int Total { get; set; }
        public string[] Labels { get; set; }
        public string Link { get; set; }
        public string BtnName { get; set; }
        public bool HasHeader { get; set; } = true;
        public string ImageName { get; set; }
        public IEnumerable<DashboardListMember> Data { get; set; }
    }

    public class DashboardListMember
    {
        public long Id { get; set; }
        public string EnumType { get; set; }
        public string Key { get; set; }
        public int Value { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string MonthIndex { get; set; }
        public string IconName { get; set; }
    }
}
