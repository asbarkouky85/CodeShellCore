using Asga.Auth.Services;
using CodeShellCore;
using CodeShellCore.Cli;
using CodeShellCore.Files;
using CodeShellCore.Files.Logging;
using CodeShellCore.Helpers;
using CodeShellCore.Http.Pushing;
using CodeShellCore.Linq;
using CodeShellCore.Localization;
using CodeShellCore.Moldster.Builder;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Services.Email;
using ConsoleTest1.Models;
using ExampleProject.Commander.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTest1
{
    public class TestConsoleController : ConsoleController
    {
        public override Dictionary<int, string> Functions => new Dictionary<int, string>
        {
            { 1,"TestLogger"},
            { 2,"TestId" },
            { 3,"TestFCM"},
            { 4,"TestAttributes"},
            { 5,"TestEmail"},
            { 6,"TestExpressions"},
            { 7,"TestLookups"},
            { 8,"TestUtils"},
            { 9,"TestFixPages"}
        };

        public void TestFixPages()
        {
            var ser = Injector.GetService<ILocalizationService>();
            ser.FixPages("ClientApp");
        }

        public void TestUtils()
        {
            var acc = new EnvironmentAccessor();
            acc.CurrentEnvironment = new MoldsterEnvironment
            {
                Upload = new CodeShellCore.Net.UploadConfig
                {
                    ServerUrl = "http://i-maher.com:8019"
                }
            };
            var ser = new PublisherHttpService(acc);
            var res = ser.HandleRequest(new CodeShellCore.Net.PublisherRequest
            {
                Type = CodeShellCore.Net.ServerRequestTypes.Decompress,
                FileName = "wwwroot/dist/ClientApp-v1.0.6.1.zip",
                DestinationFolder = "wwwroot/dist/v1.0.6.1"
            });
        }


        public void TestLookups()
        {
            var s = Injector.GetService<IAuthLookupService>();
            var data = s.GetRequestedLookups(new Dictionary<string, string> { { "users", "c0" } });

        }

        public void TestExpressions()
        {
            var gen = Expressions.GetStringContainsFilter<Person>("Id", "ah");
        }

        public void TestId()
        {
            for (var i = 0; i < 100; i++)
            {
                var id = Utils.GenerateIntID();
                Console.WriteLine(id);
            }
        }

        public void TestAttributes()
        {
            var attrs = (EntityNameAttribute)typeof(TestDto).GetCustomAttributes(true).FirstOrDefault(e => e.GetType().IsAssignableFrom(typeof(EntityNameAttribute)));
            if (attrs != null)
                Console.WriteLine(attrs.EntityName);
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
            var s = ser.SendNotification(new FirebaseMessage
            {
                title = "title",
                body = "body"
            },
                topics: new[] { "Offers" }
            );
        }

        public void TestLogger()
        {
            //Logger.Default.TextWriter.MaxLines = 100;
            //Thread th1 = new Thread(ThreadTest);
            //Thread th2 = new Thread(ThreadTest);
            //Thread th3 = new Thread(ThreadTest);
            //th1.Start();
            //Thread.Sleep(500);
            //th2.Start();
            //Thread.Sleep(500);
            //th3.Start();
            //Thread.Sleep(500);
            var l = Logger.Create("Test", "./");
            l.Write("hi");
            l.WriteLogLine("");
            l.Write("hi2");
            l.GotoColumn(3);
            l.WriteLogLine("hhh");
            //th2.Start();
        }

        public void TestEmail()
        {
            var srv = Injector.GetService<EmailService>();
            var cl = srv.CreateClient();
            var mail = srv.CreateMessage("asbarkouky@gmail.com", "Subject Test", "", true);
            var byts = new FileBytes[] { new FileBytes(@"C:\ASGA_TFS\Libraries\CodeShellCore\master\Configurator.UI\wwwroot\img\default_user.png") };
            mail.Body = "<h1>Hi</h1><img src=\"%A0%\" />";
            srv.AppendAttachments(mail, byts);
            var res = srv.SendEmail(cl, mail);
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
