using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CodeShellCore.Moldster;
using System;

namespace GenerationTest.Api.Data
{
    public static class TestConfigData
    {
        public static void AddTestConfigDB(this IServiceCollection coll)
        {
            coll.Remove(new ServiceDescriptor(typeof(MoldsterContext), typeof(MoldsterContext), ServiceLifetime.Scoped));
            coll.AddDbContext<MoldsterContext>(d => d.UseInMemoryDatabase("m"));
        }

        public static void Initialize(IServiceProvider prov)
        {

            MoldsterContext cont = prov.GetService<MoldsterContext>();

            Domains(cont);
            Tenants(cont);
            Resources(cont);
            PageCategories(cont);
            Pages(cont);
            cont.SaveChanges();

        }

        private static void Tenants(MoldsterContext cont)
        {
            cont.Tenants.Add(new Tenant { Id = 1, Name = "Client", Code = "Client", MainComponentBase = "AppComponent", BaseStyle = "client-app.scss" });
            cont.Tenants.Add(new Tenant { Id = 2, Name = "Admin", Code = "Admin", MainComponentBase = "AppComponent" });
        }

        public static void Domains(MoldsterContext cont)
        {
            cont.Domains.Add(new Domain { Id = 1, Name = "Auth", NameChain = "/Auth/", Chain = "|1|" });
            cont.Domains.Add(new Domain { Id = 2, Name = "Users", NameChain = "/Auth/Users/", Chain = "|1|2|", ParentId = 1 });
        }

        public static void Resources(MoldsterContext cont)
        {
            cont.Resources.Add(new Resource { Id = 1, Name = "Users", DomainId = 1 });
        }

        public static void PageCategories(MoldsterContext cont)
        {

            cont.PageCategories.Add(new PageCategory
            {
                Id = 1,
                Name = "Test",
                BaseComponent = "List",
                ViewPath = "Test",
                DomainId = 2,
                ResourceId = 2,
            });
        }

        public static void Pages(MoldsterContext cont)
        {
            cont.Pages.Add(new Page
            {
                Id = 1,
                Name = "Test",
                ViewPath = "Test",
                DomainId = 2,
                TenantId = 1,
                ResourceId = 1,
                SpecialPermission = "anonymous",
                PageCategoryId = 1,
                Layout = "Layout/EditLayout",
                HasRoute = true,
                IsHomePage = true
            });


        }


    }
}
