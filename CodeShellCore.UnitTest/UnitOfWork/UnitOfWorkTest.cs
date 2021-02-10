using Asga.Web;
using Asga.Auth;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using CodeShellCore.Web.UnitTest;
using Microsoft.EntityFrameworkCore;
using CodeShellCore.UnitTest.Data;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Data.EntityFramework;
using Asga.Common.Data;
using Asga.Common.Services;

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
