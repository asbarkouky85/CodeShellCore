using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Services;
using CodeShellCore.Moldster.Data;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Moldster.Domains
{
    public class DomainService : EntityService<Domain>
    {
        protected IConfigUnit Unit;

        public DomainService(IConfigUnit unit) : base(unit)
        {
            Unit = unit;
        }



        public Dictionary<long, int> PageCategoryCounters()
        {
            var l = Unit.DomainRepository.FindAs(
                d => new { d.Id, Count = d.PageCategories.Count() },
                d => d.PageCategories.Any()
                );
            return l.ToDictionary(d => d.Id, d => d.Count);
        }

        public Dictionary<long, int> PageCounters(long id)
        {
            var l = Unit.DomainRepository.FindAs(
                d => new { d.Id, Count = d.Pages.Where(e => e.TenantId == id).Count() },
                d => d.Pages.Where(e => e.TenantId == id).Any()
                );
            return l.ToDictionary(d => d.Id, d => d.Count);
        }

        void CheckForShared()
        {
            Unit.DomainRepository.Merge(d => d.Name == "Shared", new Domain { Id = 1, Name = "Shared" });
            Unit.SaveChanges();
        }

        public IEnumerable<Domain> GetTree(long? tenantId = null)
        {
            CheckForShared();
            if (tenantId == null)
                return Unit.DomainRepository.GetList().Recurse().ToList();
            else
                return Unit.DomainRepository.GetHavingPagesForTenant(tenantId.Value).Recurse().ToList();
        }

        public IEnumerable<Domain> GetCategoriesTree()
        {
            CheckForShared();
            return Unit.DomainRepository.GetHavingCategories().Recurse().ToList();
        }

        public long GetDomainId(string domain)
        {
            if (!Unit.DomainRepository.Exist(d => d.Name == domain))
            {
                Domain d = new Domain
                {
                    Name = domain
                };
                Unit.DomainRepository.Add(d);
                Unit.SaveChanges();
                return d.Id;
            }
            else
            {
                return Unit.DomainRepository.GetSingleValue(e => e.Id, d => d.Name == domain);
            }

        }

    }
}
