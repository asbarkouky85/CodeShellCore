using NUnit.Framework;
using System;

using CodeShellCore.Moldster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CodeShellCore.UnitTest.Data;
using CodeShellCore.Web.Razor;

namespace CodeShellCore.UnitTest.Moldster
{
    [TestFixture]
    public class DomainServiceTest : UnitTestClass
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
        public void GetDomainByPath_ExistingPath_ReturnsCorrectId()
        {
            RunScoped(sc =>
            {
                string domain = "Auth/Users";
                IConfigUnit unit = sc.GetService<IConfigUnit>();
                var dom = unit.DomainRepository.GetDomainByPath(domain);
                Assert.That(dom.Id == 2, () => $"Expected id 2 result is {dom.Id}");
            });
        }

        [Test]
        public void GetDomainByPath_NonExistant_ReturnsNull()
        {
            RunScoped(sc =>
            {
                string domain = "Roles";
                IConfigUnit unit = sc.GetService<IConfigUnit>();
                var dom = unit.DomainRepository.GetDomainByPath(domain);
                Assert.That(dom == null);
            });
        }

        [Test]
        public void GetDomainByPath_BadRequest_ShouldThrowException()
        {
            RunScoped(sc =>
            {
                string domain = "";
                IConfigUnit unit = sc.GetService<IConfigUnit>();

                Assert.Throws(typeof(ArgumentException), () =>
                {
                    var dom = unit.DomainRepository.GetDomainByPath(domain);
                });
            });
        }

        [Test]
        [TestCase("Auth/Roles/Modals", 2)]
        [TestCase("WorkForce/JobRoles/Modals", 3)]
        [TestCase("Auth/Users/Modals", 1)]
        public void CreatePathAndGetId_CreatesPath(string dom, int affected)
        {
            RunScoped(sc =>
            {
                var unit = sc.GetService<IConfigUnit>();
                var domain = unit.DomainRepository.GetOrCreatePath(dom);
                var res = unit.SaveChanges();
                Assert.AreEqual(affected, res.AffectedRows);
                Assert.That(domain != null);
            });
        }


    }
}
