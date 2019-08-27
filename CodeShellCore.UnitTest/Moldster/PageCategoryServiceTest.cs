﻿using CodeShellCore.Moldster.Db;
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
        [TestCase("Auth/Users/UserList",1)]
        [TestCase("Auth/Roles/RoleList", 2)]
        public void Create_Cases(string viewPath,int rows)
        {
            RunScoped(sc =>
            {
                PageCategory cat = new PageCategory
                {
                    DomainId = 2,
                    Name = "",
                    ViewPath = viewPath,
                    ResourceName = "Users",
                    BaseComponent = "Edit"
                };

                var fileMock = new Mock<IFileHandler>();
                fileMock.Setup(d => d.Exists(".\\Views\\"+viewPath+".cshtml")).Returns(true);
                var unit = sc.GetService<IConfigUnit>();
                var service = new PageCategoryService(unit, fileMock.Object);

                var res = service.Create(cat);
                var x = sc.GetService<MoldsterContext>();
                Assert.AreEqual(rows, res.AffectedRows);
            });

        }
    }
}