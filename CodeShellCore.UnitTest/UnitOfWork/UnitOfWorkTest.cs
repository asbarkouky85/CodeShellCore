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
                coll.AddScoped<TestUnit>();
                coll.AddTransient(typeof(TestRepository<,>));
                coll.AddDbContext<AuthContext>(d => d.UseInMemoryDatabase("mydb"));
                coll.AddGenericRepository(typeof(Repository_Int64<,>));
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
