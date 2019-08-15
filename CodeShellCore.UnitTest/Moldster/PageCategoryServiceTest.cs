using CodeShellCore.Moldster.Db;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.UnitTest.Data;
using CodeShellCore.Moldster;
using Microsoft.EntityFrameworkCore;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Moldster.Db.Services;
using CodeShellCore.Web.Razor;
using CodeShellCore.Files;
using CodeShellCore.Text;
using Moq;

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
                coll.AddMoldsterWeb(MoldsType.Db);
                coll.AddDbContext<MoldsterContext>(d => d.UseInMemoryDatabase("moldster"));
                coll.AddScoped<MoldsterDataInit>();
            }));
            RunScoped(sc => sc.GetService<MoldsterDataInit>().InitilizeDomains());
        }



        [Test]
        public void Create_HappyScenario_ShouldSave()
        {
            RunScoped(sc =>
            {
                PageCategory cat = new PageCategory
                {
                    DomainId = 2,
                    Name = "",
                    ViewPath = "Auth/Users/UserEdit",
                    ResourceName = "Users",
                    BaseComponent = "Edit"
                };

                var fileMock = new Mock<IFileHandler>();
                fileMock.Setup(d => d.Exists(".\\Views\\Auth/Users/UserEdit.cshtml")).Returns(true);
                var unit = sc.GetService<IConfigUnit>();
                var service = new PageCategoryService(unit, fileMock.Object, sc.GetService<DomainService>());

                var res = service.Create(cat);
                var x = sc.GetService<MoldsterContext>();
                Assert.That(res.AffectedRows == 1);
            });

        }
    }
}
