using CodeShellCore.Helpers;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Services
{
    public interface IMigrationService
    {
        Task<Result> MigrateBaseModule(string tenant);
        Task<Result> CategoriesToComponents(string tenantCode);
        Task<Result> RestructureApp(string tenantCode);
    }
}