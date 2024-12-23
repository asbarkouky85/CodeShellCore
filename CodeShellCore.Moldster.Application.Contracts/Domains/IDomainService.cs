using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Services;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Domains
{
    public interface IDomainService : IDtoEntityService<long, DomainListDto, PagedListRequestDto, DomainDto, DomainDto>
    {
        Task<List<DomainListDto>> GetCategoriesTree();
        Task<long> GetDomainId(string domain);
        Task<List<DomainListDto>> GetTenantTree(long tenantId);
        Task<List<DomainListDto>> GetTree();
        Task<Result> InstallModule(string assemblyName);
        Task<Dictionary<long, int>> PageCategoryCounters();
        Task<Dictionary<long, int>> PageCounters(long id);
        Task<Result> UpdateFiles(string assemblyName);
    }
}