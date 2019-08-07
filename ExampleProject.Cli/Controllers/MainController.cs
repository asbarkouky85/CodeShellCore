using Asga.Auth;
using Asga.Security;
using CodeShellCore.Caching;
using CodeShellCore.Cli;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleProject.Cli.Controllers
{

    public class Hamada
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class MainController : ConsoleController
    {
        public override Dictionary<int, string> Functions => new Dictionary<int, string>
        {
            { 1,"Test"}
        };

        public void Test()
        {
            var cach = Injector.GetService<ICacheProvider>();
            UserDTO dto = new UserDTO
            {
                UserId = 3,
                Apps = new[] { "Admin" }
            };
            var dt = cach.Get<UserDTO>("3");
        }
    }
}
