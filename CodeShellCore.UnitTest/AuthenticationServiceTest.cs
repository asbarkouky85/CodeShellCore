// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using Asga.Auth;
using Asga.Web;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Cryptography;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Text;
using CodeShellCore.Web.UnitTest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;

namespace CodeShellCore.UnitTest
{

    [TestFixture]
    public class AuthenticationServiceTest : UnitTestClass
    {
        public AuthenticationServiceTest()
        {
            var sh = new UnitTestShell(coll =>
           {
               coll.AddAuthModule();
               coll.AddAsgaWeb();

               coll.AddTransient<IHttpContextAccessor, TestHttpContextAccessor>();
               coll.AddSingleton(d => new Encryptor("123"));
               coll.AddContext<AuthContext>();
               coll.AddDbContext<AuthContext>(d => d.UseInMemoryDatabase("mydb"));
           });
            Shell.Start(sh);
            InitializeDb();
        }
        private void InitializeDb()
        {

            using (var sc = Shell.GetScope())
            {
                var c = sc.ServiceProvider.GetService<AuthContext>();
                c.Resources.Add(new Resource
                {
                    Id = 1,
                    Name = "Users"
                });

                c.Resources.Add(new Resource
                {
                    Id = 2,
                    Name = "Roles"
                });

                c.Roles.Add(new Role
                {
                    Id = 1,
                    Name = "Admin",
                    RoleResources = new List<RoleResource>
                {
                    new RoleResource { Id=1,ResourceId=1,CanViewDetails=true,CanDelete=true },
                    new RoleResource { Id=2,ResourceId=2,CanViewDetails=true,CanDelete=false }
                }
                });
                c.Users.Add(new User
                {
                    Id = 1,
                    LogonName = "admin",
                    Password = "12345".ToMD5(),
                    UserRoles = new List<UserRole> {
                    new UserRole{ Id=1,RoleId=1}
                }
                });
                c.SaveChanges();
            }
        }

        [Test]
        public void UserAccessorWorks()
        {
            RunTest(sc =>
            {
                var auth = sc.GetService<IAuthenticationService>();
                var sess = sc.GetService<ISessionManager>();
                var res = auth.Login("admin", "12345");

                sess.AuthorizationRequest(res.Token);

                var acc = sc.GetService<IUserAccessor>();
                Assert.That(acc.User != null && acc.User.UserId.Equals((long)1));
            });
        }

        [Test]
        [TestCase("admin", "12345", true)]
        [TestCase("admin", "345", false)]
        public void Login_Test(string userName, string password, bool success)
        {
            RunTest(sc =>
            {
                var auth = sc.GetService<IAuthenticationService>();
                var res = auth.Login(userName, password);
                Assert.That(res.Success == success, () => res.Message);
            });
        }

        [Test]
        [TestCase("Users", "CanViewDetails", true)]
        [TestCase("Roles", "CanViewDetails", true)]
        [TestCase("Roles", "CanDelete", false)]
        public void Authorization(string resource, string method, bool res)
        {
            RunTest(sc =>
            {
                var auth = sc.GetService<IAuthenticationService>();
                var sess = sc.GetService<ISessionManager>();
                var lRes = auth.Login("admin", "12345");

                sess.AuthorizationRequest(lRes.Token);
                var zie = sc.GetService<IAuthorizationService>();
                
                IAccessControlAuthorizationService acc = (IAccessControlAuthorizationService)zie;
                AuthorizationRequest<AuthorizationFilterContext> req = new AuthorizationRequest<AuthorizationFilterContext>(null);
                req.Resource = resource;
                req.Action = method;
                var resp = acc.IsAuthorized(req);

                Assert.AreEqual(res, resp);
            });
        }
    }
}
