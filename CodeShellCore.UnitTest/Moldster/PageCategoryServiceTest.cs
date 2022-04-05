using CodeShellCore.Files;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.PageCategories.Dtos;
using CodeShellCore.UnitTest.Data;
using CodeShellCore.Web.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

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
                coll.Services.AddMoldsterDbData(coll.Configuration);
                coll.Services.AddMoldsterCodeGeneration();
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
                PageCategoryDto cat = new PageCategoryDto
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
                var res = service.Post(cat);
                var pageCatId = res.Result.Id;
                var pageCategory = unit.PageCategoryRepository.FindSingle(pageCatId);

                Assert.AreEqual(rows, res.AffectedRows);
                Assert.AreNotEqual(cat.DomainId, null);
                //Assert.That(viewPath.Contains(cat.Domain.Name), "view path doesn't contain domain name");
            });

        }
    }
}
