using CodeShellCore.Files.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

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
