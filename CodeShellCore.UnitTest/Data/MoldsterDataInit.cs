using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Resources;

namespace CodeShellCore.UnitTest.Data
{
    public class MoldsterDataInit
    {
        private readonly MoldsterContext context;

        public MoldsterDataInit(MoldsterContext context)
        {
            this.context = context;
        }
        public void InitilizeDomains()
        {
            context.Tenants.Add(new Tenant { Id = 1, Code = "Main", Name = "Name" });
            context.Domains.Add(new Domain { Id = 1, Name = "Auth", NameChain = "/Auth/", Chain = "|1|" });
            context.Domains.Add(new Domain { Id = 2, Name = "Users", NameChain = "/Auth/Users/", ParentId = 1, Chain = "|1|2|" });
            context.NavigationGroups.Add(new NavigationGroup { Id = 1, Name = "TopBar" });
            context.Resources.Add(new Resource { Id = 1, Name = "Users", DomainId = 1 });
            context.SaveChanges();
        }

        public void InitializeTemplates()
        {
            context.PageCategories.Add(new PageCategory { Id = 1, ViewPath = "Auth/Users/UserEdit", BaseComponent = "Edit", DomainId = 2 });

            context.SaveChanges();
        }
    }
}
