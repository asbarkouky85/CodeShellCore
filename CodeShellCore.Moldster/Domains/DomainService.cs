using CodeShellCore.Data;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Recursion;
using CodeShellCore.Data.Services;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Domains
{
    public class DomainService : DtoEntityService<Domain, long, DomainListDto, PagedListRequestDto, DomainDto, DomainDto>, IDomainService
    {
        protected IMoldsterUnit Unit;
        IMoldsterLookupService _lookups => Store.GetService<IMoldsterLookupService>();
        IModulesService _modules => Store.GetService<IModulesService>();

        public DomainService(IMoldsterUnit unit) : base(unit)
        {
            Unit = unit;
        }

        public async Task<Dictionary<long, int>> PageCategoryCounters()
        {
            var l = await Unit.DomainRepository.FindAs(
                d => new { d.Id, Count = d.PageCategories.Count() },
                d => d.PageCategories.Any()
                );
            return l.ToDictionary(d => d.Id, d => d.Count);
        }

        public async Task<Dictionary<long, int>> PageCounters(long id)
        {
            var l = await Unit.DomainRepository.FindAs(
                d => new { d.Id, Count = d.Pages.Where(e => e.TenantId == id).Count() },
                d => d.Pages.Where(e => e.TenantId == id).Any()
                );
            return l.ToDictionary(d => d.Id, d => d.Count);
        }

        async Task CheckForShared()
        {
            await Unit.DomainRepository.Merge(d => d.Name == "Shared", new Domain { Id = 1, Name = "Shared" });
            await Unit.SaveChanges();
        }

        public async Task<List<DomainListDto>> GetTree()
        {
            await CheckForShared();
            List<Domain> domains = await Unit.DomainRepository.GetList();
            var result = Mapper.Map(domains, new List<DomainListDto>());
            return result.Recurse();
        }


        public async Task<List<DomainListDto>> GetTenantTree(long tenantId)
        {
            await CheckForShared();
            List<Domain> domains = await Unit.DomainRepository.GetHavingPagesForTenant(tenantId);
            var result = Mapper.Map(domains, new List<DomainListDto>());

            return result.Recurse();
        }

        public async Task<List<DomainListDto>> GetCategoriesTree()
        {
            await CheckForShared();
            var domains = await Unit.DomainRepository.GetHavingCategories();
            var result = Mapper.Map(domains, new List<DomainListDto>());
            return result.Recurse();
        }

        public async Task<long> GetDomainId(string domain)
        {
            if (!(await Unit.DomainRepository.Exist(d => d.Name == domain)))
            {
                Domain d = new Domain
                {
                    Name = domain
                };
                Unit.DomainRepository.Add(d);
                await Unit.SaveChanges();
                return d.Id;
            }
            else
            {
                return await Unit.DomainRepository.GetSingleValue(e => e.Id, d => d.Name == domain);
            }

        }

        public override Task<Dictionary<string, IEnumerable<Named<object>>>> GetEditLookups(Dictionary<string, string> dto)
        {
            return _lookups.Modules(dto);
        }

        public async Task<Result> UpdateFiles(string assemblyName)
        {
            return await _modules.UpdateModuleFiles(assemblyName);
        }

        public async Task<Result> InstallModule(string assemblyName)
        {
            return await _modules.InstallModule(assemblyName);
        }

    }


}
