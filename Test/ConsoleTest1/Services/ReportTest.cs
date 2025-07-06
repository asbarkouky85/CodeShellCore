using CodeShellCore.Reporting;
using System.Collections.Generic;

namespace ExampleProject.Commander.Services
{
    public class ReportTest : ReportModel
    {
        public override string Template => "ReportTest";
        public IEnumerable<ReportTestItem> Users { get; set; }
    }

    public class ReportTestItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
