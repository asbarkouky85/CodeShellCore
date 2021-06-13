﻿using Asga.Auth;
using Asga.Auth.Services;
using Asga.Web;
using CodeShellCore.Caching;
using CodeShellCore.Security.Authorization;
using CodeShellCore.UnitTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.UnitTest.Auth
{
    [TestFixture]
    public class RoleServiceTest : UnitTestClass
    {
        [SetUp]
        public void SetUp()
        {
            var sd = new UnitTestShell(context =>
            {
                context.Services.AddAsgaAuthModule(context.Configuration, false);
                context.Services.AddAsgaAuthWeb();
                context.Services.AddTransient<AuthDataInit>();
                context.Services.AddDbContext<AuthContext>(d => d.UseInMemoryDatabase("mydb"));
            });
            Shell.Start(sd);
            RunScoped(prov =>
            {
                prov.GetService<AuthDataInit>().InitializeForRoles();
            });

        }

        [Test]
        public void Update_ClearCache()
        {
            var role = new Role
            {
                Id = 1,
                Name = "Admin"

            };
            RunScoped(prov =>
            {
                var s = prov.GetService<ICacheProvider>();
                s.Store<RoleCacheItem>("1", new RoleCacheItem
                {
                    RoleId = 1


                });
                prov.GetService<RolesService>().Update(role);
                var itemCached = s.Get<RoleCacheItem>("1");
                Assert.AreEqual(null, itemCached);
            });
        }
    }
}
