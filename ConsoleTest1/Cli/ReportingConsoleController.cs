using CodeShellCore.Cli;
using CodeShellCore.Files.Reporting;
using ExampleProject.Commander.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest1
{
    public class ReportingConsoleController : ConsoleController
    {
        RdlcDataSetGenerator generator => GetService<RdlcDataSetGenerator>();
        public override Dictionary<int, string> Functions => new Dictionary<int, string>
        {
            { 1,"BindReports"}
        };

        public void BindReports()
        {
            generator.Bind(new ReportTest());
        }
    }
}
