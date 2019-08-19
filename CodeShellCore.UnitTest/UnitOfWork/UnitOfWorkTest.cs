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
                coll.AddAuthModule();
                coll.AddAsgaWeb();

                coll.AddTransient<IHttpContextAccessor, TestHttpContextAccessor>();

                coll.AddDbContext<AuthContext>(d => d.UseInMemoryDatabase("mydb"));
                coll.AddScoped<AuthDataInit>();
            }));
        }
    }
}
