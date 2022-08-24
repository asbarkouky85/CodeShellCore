using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Web.UnitTest;
using CodeShellCore.UnitTest.Data;

using Asga.Auth;
using Asga.Web;
using Asga.Security;

namespace CodeShellCore.UnitTest.Auth
{

    [TestFixture]
    public class AuthenticationServiceTest : UnitTestClass
    {
        [SetUp]
        public void StartUp()
        {
            Shell.Start(new UnitTestShell(context =>
            {
                //context.Services.AddCodeShellApplication
                context.Services.AddAsgaAuthModule(context.Configuration);
                context.Services.AddAsgaAuthWeb();

                context.Services.AddScoped<IHttpContextAccessor>(d => new TestHttpContextAccessor(d));

                context.Services.AddDbContext<AuthContext>(d => d.UseInMemoryDatabase("mydb"));
                context.Services.AddScoped<AuthDataInit>();
            }));
            RunScoped(sc => sc.GetService<AuthDataInit>().IntitializeUsersData());
        }

        [Test]
        public void IUserAccessor()
        {
            string token = "";
            RunScoped(sc =>
            {
                var auth = sc.GetService<IAuthenticationService>();
                var sess = sc.GetService<ISessionManager>();
                var res = auth.Login("admin", "12345");
                token = res.Token;
                Assert.That(token != null);
            });

            RunScoped(sc =>
            {
                // UnitTestShell.CurrentScope = null;
                var sess = sc.GetService<ISessionManager>();
                sess.AuthorizationRequest(token);

                var acc = sc.GetService<IUserAccessor>();
                Assert.That(acc.User != null && acc.User.UserId.Equals(1.ToString()));
                Assert.IsInstanceOf<UserDTO>(acc.User, "User is not UserDTO");
                var dto = (UserDTO)acc.User;
                Assert.AreEqual(2, dto.Permissions.Count, "Permission count is incorrect");
                Assert.AreEqual(new[] { 3, 4 }, dto.EntityLinks["Customer"]);
                Assert.AreEqual(new[] { 10 }, dto.EntityLinks["Warehouse"]);
            });
        }

        [Test]
        [TestCase("admin", "12345", true)]
        [TestCase("admin", "345", false)]
        public void Login_Cases(string userName, string password, bool success)
        {
            RunScoped(sc =>
            {
                var auth = sc.GetService<IAuthenticationService>();
                var res = auth.Login(userName, password);
                Assert.That(res.IsSuccess == success, () => res.Message);
            });
        }

        [Test]
        [TestCase("Users", "CanViewDetails", true)]
        [TestCase("Roles", "CanViewDetails", true)]
        [TestCase("Roles", "CanDelete", false)]
        public void IsAuthorized_Cases(string resource, string method, bool res)
        {
            RunScoped(sc =>
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

        [TearDown]
        public void Cleanup()
        {
            RunScoped(sc =>
            {
                sc.GetService<AuthContext>().Database.EnsureDeleted();
            });
        }
    }
}
