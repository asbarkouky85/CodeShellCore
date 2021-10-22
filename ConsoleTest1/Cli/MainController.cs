using System.Collections.Generic;

using CodeShellCore.Cli;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.MQ;
using CodeShellCore.MQ.Events;

namespace ConsoleTest1
{
    public class MainController : ConsoleController
    {
        public override Dictionary<int, string> Functions => new Dictionary<int, string>
        {
            { 1,"Reporting" },
            { 2,"AngularJson" },
            { 3,"Tests" },
            { 4,"SendEvent" }
        };

        public void Reporting()
        {
            var con = new ReportingConsoleController();
            con.Run();
        }

        public void AngularJson()
        {
            var ser = GetService<IAngularJsonService>();
           ser.UpdateFileFromDatabase();
        }

        public void Tests()
        {
            var con = new TestConsoleController();
            con.Run();
        }

        public void SendEvent()
        {
            Transporter.Publish(new SimpleEvent { Id = 1, Message = "hiiiii" });
            Transporter.Publish(new SimpleEvent2 { Id = 2, Message = "hiiiii2" });
        }
    }
}
