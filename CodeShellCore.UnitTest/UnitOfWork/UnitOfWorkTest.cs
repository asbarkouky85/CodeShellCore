using Asga.Auth;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Data.EntityFramework;

namespace CodeShellCore.UnitTest.UnitOfWork
{
    [TestFixture]
    public class UnitOfWorkTest : UnitTestClass
    {
        [SetUp]
        public void SetUp()
        {
            Shell.Start(new UnitTestShell(coll =>
            {
                coll.Services.AddScoped<TestUnit>();
                coll.Services.AddTransient(typeof(TestRepository<,>));
                coll.Services.AddDbContext<AuthContext>(d => d.UseInMemoryDatabase("mydb"));
                coll.Services.AddGenericRepository(typeof(Repository_Int64<,>));
            }));
        }

        [Test]
        public void GetRepositoryFor_GetsTheCorrectType()
        {
            RunScoped(sc =>
            {

                var unit = sc.GetService<TestUnit>();

                var repo = unit.GetRepositoryFor<UserRole>();

                Assert.IsInstanceOf<TestRepository<UserRole, AuthContext>>(repo);
            });
        }
    }
}
