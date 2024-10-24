using CodeShellCore.Helpers;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Services
{
    public interface IMigrationService
    {
        Result MigrateBaseModule(string tenant);
        Task<Result> RestructureApp(string tenantCode);
    }
}