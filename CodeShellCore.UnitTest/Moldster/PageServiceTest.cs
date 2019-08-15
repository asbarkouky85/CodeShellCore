using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Moldster.Db.Services;
using CodeShellCore.UnitTest.Data;
using CodeShellCore.Web.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.UnitTest.Moldster
{
    public class PageServiceTest : UnitTestClass
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
            RunScoped(sc =>
            {
                var service = sc.GetService<MoldsterDataInit>();
                service.InitilizeDomains();
                service.InitializeTemplates();
            });
        }


        [Test]
        [TestCase("Main", "Auth/Users/UserCreate", "Auth/Users/UserEdit", "Users", "insert", 0, 1)]
        [TestCase("Main", "Auth/Users/UserCreate", "Auth/Users/UserEdit", "Users", "Kill", 0, 2)]

        [TestCase("Main", "Auth/Users/Modals/UserEditModal", "Auth/Users/UserEdit", "Users", "insert", 0, 1)]
        public void Create_Cases(string tenant, string viewPath, string template, string resource, string actionType, int code, int rows)
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
                    Usage = "R"
                };
                var service = p.GetService<PagesService>();
                var unit = p.GetService<IConfigUnit>();
                var res = service.Create(dto);

                var existingDomains = unit.DomainRepository.GetValues(d => d.Id).ToList();
                TestContext.WriteLine(res.Message);
                Assert.AreEqual(code, res.Code, "Code is incorrect");
                Assert.AreEqual(rows, res.AffectedRows, "Affected rows is incorrect");

                if (res.Code == 0)
                {
                    Page page = (Page)res.Data["entity"];
                    Assert.Contains(page.DomainId, existingDomains, "domain id not in list", null);
                }


            });

        }
    }
}
