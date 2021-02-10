using System.Collections.Generic;

using CodeShellCore.Cli;

namespace ConsoleTest1
{
    public class MainController : ConsoleController
    {
        public override Dictionary<int, string> Functions => new Dictionary<int, string>
        {
            { 1,"Reporting"},
            { 2,"Tests"}
        };
        
        public void Reporting()
        {
            var con = new ReportingConsoleController();
            con.Run();
        }

        public void Tests()
        {
            var con = new TestConsoleController();
            con.Run();
        }
    }
}
