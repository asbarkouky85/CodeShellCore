using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Configurator.Services;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.UnitTest.Data;
using CodeShellCore.Web.Razor;
using CodeShellCore.Web.UnitTest;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;

namespace CodeShellCore.UnitTest.Moldster
{
    public class PageServiceTest : UnitTestClass
    {
        [SetUp]
        public void SetUp()
        {
            Shell.Start(new UnitTestShell(coll =>
            {
                coll.Services.AddMoldsterServerGeneration();
                coll.Services.AddMoldsterWeb();
                coll.Services.AddDbContext<MoldsterContext>(d => d.UseInMemoryDatabase("moldster"));
                coll.Services.AddScoped<MoldsterDataInit>();
                coll.Services.AddScoped<IHttpContextAccessor, TestHttpContextAccessor>();
            }));
            RunOnce(sc =>
            {
                var service = sc.GetService<MoldsterDataInit>();
                service.InitilizeDomains();
                service.InitializeTemplates();
            });
        }


        [Test]
        [TestCase("Main", "Auth/Users/UserCreate", "Auth/Users/UserEdit", "Users", "insert", null, 1)]
        [TestCase("Main", "Auth/Users/UserCreate", "Auth/Users/UserEdit", "Users", "Kill", null, 2)]
        [TestCase("Main", "Auth/Users/Modals/UserEditModal", "Auth/Users/UserEdit", "Users", "insert", null, 2)]
        [TestCase("Main", "Auth/Users/UserDetails", "Auth/Users/UserEdit", "Users", "insert", "TopBar", 2)]
        [TestCase("Main", "Auth/Users/UserInfo", "Auth/Users/UserEdit", "Users", "insert", "SideBar", 3)]
        [TestCase("Main", "Auth/Users/UserInfo2", "Auth/Users/UserEdit", "Auth/Users", "insert", "SideBar", 3)]
        [TestCase("Main", "Auth/Users/UserInfo3", "Auth/Users/UserEdit", "Admin/Users", "insert", "SideBar", 5)]
        public void Create_Using(string tenant, string viewPath, string template, string resource, string actionType, string navGroup, int rows)
        {
            RunScoped(p =>
            {
                CreatePageDTO dto = new CreatePageDTO
                {
                    TenantCode = tenant,
                    ComponentPath = viewPath,
                    TemplatePath = template,
                    Resource = resource,
                    ActionType = actionType,
                    Usage = "R",
                    NavigationGroup = navGroup,
                    Apps = new[] { "Admin" }
                };
                var service = p.GetService<PagesService>();
                var unit = p.GetService<IConfigUnit>();
                Page page = null;
                unit.Saving += (u, lst) =>
                {
                    page = (Page)lst.Added.Where(d => d is Page).FirstOrDefault();
                };
                var res = service.Create(dto);

                var existingDomains = unit.DomainRepository.GetValues(d => d.Id).ToList();
                TestContext.WriteLine(res.Message);

                Assert.AreEqual(rows, res.AffectedRows, "Affected rows is incorrect");
                if (rows > 0)
                {
                    Assert.IsNotNull(page);
                    Assert.AreEqual("\"Admin\"", page.Apps);
                }


            });

        }
    }
}
