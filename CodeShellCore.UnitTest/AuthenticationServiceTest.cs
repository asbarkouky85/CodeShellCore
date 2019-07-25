// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Asga.Auth;
using CodeShellCore;
using CodeShellCore.Data;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Text;
using CodeShellCore.Web.Security;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace CodeShellCore.UnitTest
{

    [TestFixture]
    public class AuthenticationServiceTest
    {
        public AuthenticationServiceTest()
        {
            Shell.Start(new UnitTestShell());

        }
        [Test]
        [TestCase("admin", "12345", true)]
        public void Login_Test(string userName, string password, bool success)
        {
            using (var sc = Shell.GetScope())
            {
                UnitTestShell.CurrentScope = sc;
                var c = sc.ServiceProvider.GetService<AuthContext>();
                c.Users.Add(new User
                {
                    LogonName = "admin",
                    Password = "12345".ToMD5()
                });
                c.SaveChanges();

                var ser = sc.ServiceProvider.GetService<ISecurityUnit>();
                var auth = sc.ServiceProvider.GetService<IAuthenticationService>();
                var res = auth.Login(userName, password);

                //Assert.IsInstanceOf(typeof(TokenAuthenticationService),auth);
                Assert.That(res.Success == true, () => res.Message);
            }

        }
    }
}
