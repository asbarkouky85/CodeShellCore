using CodeShellCore;
using CodeShellCore.Cli;
using CodeShellCore.Files.Logging;
using CodeShellCore.Tasks;
using CodeShellCore.Http.Pushing;
using CodeShellCore.Linq;
using CodeShellCore.Linq.Filtering;
using CodeShellCore.Text;
using ExampleProject.Commander.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest1
{
    public class TestConsoleController : ConsoleController
    {
        public override Dictionary<int, string> Functions => new Dictionary<int, string>
        {
            { 1,"TestLogger"},
            { 2,"TestInjection" },
            { 3,"TestFCM"},
            { 4,"Tasks"},
        };

        public void Tasks()
        {
            var t = new Task<int>(() =>
              {
                  Console.WriteLine("starting task");
                  Thread.Sleep(2000);
                  Console.WriteLine("done");
                  return 5;
              });
            Console.WriteLine("before call");
            t.Then(res =>
            {
                Console.WriteLine("Then action "+res);
            });
            Console.WriteLine("after call");
        }

        public void TestInjection()
        {
            using (var s = Shell.GetScope())
            {
                var sc = s.ServiceProvider.GetService<ScopedClass>();
                var t1 = s.ServiceProvider.GetService<InjectionTest>();
                var t2 = s.ServiceProvider.GetService<InjectionTest>();
            }

            using (var s = Shell.GetScope())
            {
                var t1 = s.ServiceProvider.GetService<InjectionTest>();
                var t2 = s.ServiceProvider.GetService<InjectionTest>();
            }
        }

        public void TestFCM()
        {
            PushNotificationService ser = new PushNotificationService();
            var s = ser.SendNotification(new FirebaseRequest
            {
                to = "dubks86aW8k:APA91bFT5bcXnvwghJy8E-irLddzWuGLpUvGbAQ4Fd7vsMEfpvmfC2PXFkEpKP4l3y13vEiKIGI4gpwZGRvrWW5SfXng3261AeTYjZ-U1uG_ic_LTagBunezFE0w5VTaAQtUrzye-FRO",
                notification = new FirebaseMessage
                {
                    title = "title",
                    body = "body"
                }
            });
        }

        public void TestLogger()
        {
            Logger.Default.TextWriter.MaxLines = 100;
            Thread th1 = new Thread(ThreadTest);
            Thread th2 = new Thread(ThreadTest);
            Thread th3 = new Thread(ThreadTest);
            th1.Start();
            Thread.Sleep(500);
            th2.Start();
            Thread.Sleep(500);
            th3.Start();
            Thread.Sleep(500);
            //th2.Start();
        }
        void ThreadTest()
        {
            for (var i = 0; i < 1000; i++)
            {
                CodeShellCore.Files.Logging.Logger.WriteLine("log num " + i);
            }
        }
    }
}
