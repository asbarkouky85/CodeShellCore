using CodeShellCore.Cli;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.MQ;
using CodeShellCore.MQ.Events;
using System.Collections.Generic;

namespace ConsoleTest1
{
    public class MainController : ConsoleController
    {
        public override Dictionary<int, string> Functions => new Dictionary<int, string>
        {
            { 1,"Reporting" },
            { 2,"Active" },
            { 3,"Tests" },
            { 4,"SendEvent" }
        };

        public void Reporting()
        {
            var con = new ReportingConsoleController();
            con.Run();
        }

        public void Active()
        {
            var unit = GetService<IConfigUnit>();
            var data = unit.PageRepository.GetDomainPagesForRouting<PageDetailsDto>("tenant_1", 2116407100007);
        }

        public void SendEvent()
        {
            Transporter.Publish(new SimpleEvent { Id = 1, Message = "hiiiii" });
            Transporter.Publish(new SimpleEvent2 { Id = 2, Message = "hiiiii2" });
        }
    }
}
