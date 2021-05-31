using CodeShellCore.Cli;
using CodeShellCore.MQ;
using CodeShellCore.MQ.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest2.Controllers
{
    public class MainController : ConsoleController
    {
        static int i = 1;
        public override Dictionary<int, string> Functions => new Dictionary<int, string>
        {
            { 1,"Events"}
        };

        public void Events()
        {
            var ev = new SimpleEvent { Id = i, Message = "Message number [" + (i++).ToString("D2") + "]" };
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Sending [{ev}");
            Transporter.Publish(ev);
            
        }
    }
}
