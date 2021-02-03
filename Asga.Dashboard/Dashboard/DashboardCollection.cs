using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Dashboard
{
    public class DashboardCollection : LoadResult<DashboardDTO>
    {
        public int Showing { get; set; }
        public string ListName { get; set; }
    }
}
