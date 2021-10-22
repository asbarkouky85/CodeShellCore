using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.UnitTest.Data;
using CodeShellCore.Moldster;
using Microsoft.EntityFrameworkCore;
using CodeShellCore.Web.Razor;
using CodeShellCore.Files;
using Moq;
using CodeShellCore.Moldster.Configurator.Services;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.PageCategories.Services;

namespace CodeShellCore.UnitTest.Moldster
{
    [TestFixture]
    public class PageCategoryServiceTest : UnitTestClass
    {
        [SetUp]
        public void SetUp()
        {
            Shell.Start(new UnitTestShell(coll =>
            {
                coll.Services.AddMoldsterWeb();
                coll.Services.AddDbContext<MoldsterContext>(d => d.UseInMemoryDatabase("moldster"));
                coll.Services.AddScoped<MoldsterDataInit>();
            }));
            RunOnce(sc => sc.GetService<MoldsterDataInit>().InitilizeDomains());
        }



        [Test]
        [TestCase("Auth/Users/UserList", 1)]
        [TestCase("Auth/Roles/RoleList", 2)]
        public void Create_Cases(string viewPath, int rows)
        {
            RunScoped(sc =>
            {
                PageCategory cat = new PageCategory
                {
                    Name = "",
                    ViewPath = viewPath,
                    ResourceName = "Users",
                    BaseComponent = "Edit"
                };

                var fileMock = new Mock<IFileHandler>();
                fileMock.Setup(d => d.Exists(".\\Views\\" + viewPath + ".cshtml")).Returns(true);
                var unit = sc.GetService<IConfigUnit>();
                var service = new PageCategoryService(unit, fileMock.Object, new DefaultPathsService());
                var res = service.Create(cat);

                Assert.AreEqual(rows, res.AffectedRows);
                Assert.AreNotEqual(cat.DomainId, null);
                Assert.That(viewPath.Contains(cat.Domain.Name), "view path doesn't contain domain name");
            });

        }
    }
}
